using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bussines_Logic_Layer.DTOs.Archivo;
using Bussines_Logic_Layer.Interfaces;
using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Domain_Layer.ValueObjects;
using Microsoft.AspNetCore.Hosting;

namespace Bussines_Logic_Layer.Services
{
    public class ArchivoService : IArchivoService
    {
        private readonly IArchivoRepository _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        // Define los tipos de contenido (MIME types) y extensiones permitidas
        private static readonly string[] _allowedContentTypes = new string[]
        {
            "image/png",
            "image/jpeg",
        };

        private static readonly string[] _allowedExtensions = new string[]
        {
            ".png",
            ".jpg",
            ".jpeg",
        };

        // Tamaño máximo de archivo (ej. 10 MB)
        private const long MaxFileSize = 100 * 1024 * 1024;

        public ArchivoService(IArchivoRepository repo, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _repo = repo;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<ArchivoDtoUpload?> UploadFileAsync(ArchivoDtoUpload dto)
        {
            if (dto.Archivo == null || dto.Archivo.Length == 0)
            {
                return null;
            }

            // Validaciones (repetidas aquí para el servicio, o podrías tener un validador separado)
            if (!_allowedContentTypes.Contains(dto.Archivo.ContentType.ToLower()))
            {
                throw new ArgumentException($"Tipo de archivo no permitido. Solo se permiten: {string.Join(", ", _allowedContentTypes)}.");
            }

            var fileExtension = Path.GetExtension(dto.Archivo.FileName).ToLower();
            if (!_allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException($"Extensión de archivo no permitida. Solo se permiten: {string.Join(", ", _allowedExtensions)}.");
            }

            if (dto.Archivo.Length > MaxFileSize)
            {
                throw new ArgumentException($"El archivo es demasiado grande. El tamaño máximo permitido es de {MaxFileSize / (1024 * 1024)} MB.");
            }

            try
            {
                // Obtiene la ruta base de la API Layer o la carpeta "Presentation" de forma dinámica
                var basePath = AppContext.BaseDirectory;
                var directoryInfo = new DirectoryInfo(basePath);
                var uploadsFolder = Path.Combine(directoryInfo.FullName.Split("bin")[0], "Archivos", dto.TipoEntidad.ToString(), $"{dto.TipoEntidad.ToString()} - {dto.EntidadID.ToString()}");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = string.IsNullOrWhiteSpace(dto.Nombre) ?
                                  Path.GetFileNameWithoutExtension(dto.Archivo.FileName) :
                                  dto.Nombre;

                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                var relativePath = Path.Combine("/Archivos", uniqueFileName).Replace("\\", "/");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Archivo.CopyToAsync(stream);
                }

                // Mapear de DTO de carga a entidad de base de datos
                var nuevoArchivoDb = new Archivo()
                {
                    Nombre = fileName,
                    TipoEntidad = dto.TipoEntidad,
                    EntidadID = dto.EntidadID,
                    TipoContenido = dto.Archivo.ContentType,
                    Descripcion = dto.Descripcion,
                    Ruta = filePath
                };
                await _repo.AddAsync(nuevoArchivoDb);

                // Mapear de la entidad guardada a ArchivoDto para la respuesta
                return _mapper.Map<ArchivoDtoUpload>(nuevoArchivoDb);
            }
            catch (Exception ex)
            {
                // Registrar el error (ej. usando un logger)
                Console.WriteLine($"Error al subir archivo en el servicio: {ex.Message}");
                throw; // Relanzar la excepción para que el controlador la capture y la maneje
            }
        }

        public async Task<Archivo?> GetFileByIdAsync(int idArchivo)
        {
            var archivo = await _repo.GetByIdAsync(idArchivo);
            return archivo; // El controlador necesita la entidad completa para obtener Ruta y TipoContenido
        }

        public async Task<IEnumerable<ArchivoDtoReponse>> GetFilesByEntidadAsync(int entidadId, TipoEntidadArchivo tipoEntidad)
        {
            var archivos = await _repo.GetByEntidadAsync(entidadId, tipoEntidad);
            var dtos = _mapper.Map<List<ArchivoDtoReponse>>(archivos);

            foreach (var dto in dtos)
            {
                var archivo = archivos.FirstOrDefault(a => a.idArchivo == dto.idArchivo);
                if (archivo != null && File.Exists(archivo.Ruta))
                {
                    try
                    {
                        var fileBytes = await File.ReadAllBytesAsync(archivo.Ruta);
                        dto.ArchivoBase64 = Convert.ToBase64String(fileBytes);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error leyendo archivo {archivo.Ruta}: {ex.Message}");
                        dto.ArchivoBase64 = string.Empty; // o null
                    }
                }
                else
                {
                    dto.ArchivoBase64 = string.Empty; // o null
                }
            }

            return dtos;
        }

        public async Task<ArchivoDtoReponse?> UpdateArchivoAsync(ArchivoDtoUpdate dto)
        {
            var archivoDb = await _repo.GetByIdAsync(dto.IdArchivo);
            if (archivoDb == null) return null;

            // Si se proporciona un nuevo archivo, reemplazar el existente
            if (dto.Archivo != null && dto.Archivo.Length > 0)
            {
                // Validaciones
                var extension = Path.GetExtension(dto.Archivo.FileName).ToLower();
                if (!_allowedExtensions.Contains(extension) ||
                    !_allowedContentTypes.Contains(dto.Archivo.ContentType.ToLower()))
                {
                    throw new ArgumentException("Tipo o extensión de archivo no permitido.");
                }

                if (dto.Archivo.Length > MaxFileSize)
                    throw new ArgumentException($"Archivo demasiado grande. Máx: {MaxFileSize / (1024 * 1024)} MB.");

                // Eliminar el archivo físico anterior
                if (File.Exists(archivoDb.Ruta))
                {
                    File.Delete(archivoDb.Ruta);
                }

                // Guardar el nuevo archivo
                var basePath = AppContext.BaseDirectory;
                var uploadsFolder = Path.Combine(
                    new DirectoryInfo(basePath).FullName.Split("bin")[0],
                    "Archivos",
                    archivoDb.TipoEntidad.ToString(),
                    $"{archivoDb.TipoEntidad} - {archivoDb.EntidadID}");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var nuevoNombre = Guid.NewGuid().ToString() + extension;
                var nuevaRuta = Path.Combine(uploadsFolder, nuevoNombre);
                using (var stream = new FileStream(nuevaRuta, FileMode.Create))
                {
                    await dto.Archivo.CopyToAsync(stream);
                }

                archivoDb.Ruta = nuevaRuta;
                archivoDb.TipoContenido = dto.Archivo.ContentType;
            }

            // Actualizar metadatos si vienen
            if (!string.IsNullOrWhiteSpace(dto.Nombre))
                archivoDb.Nombre = dto.Nombre;

            if (!string.IsNullOrWhiteSpace(dto.Descripcion))
                archivoDb.Descripcion = dto.Descripcion;

            await _repo.UpdateAsync(archivoDb);

            // Retornar con base64 incluido
            var fileBytes = await File.ReadAllBytesAsync(archivoDb.Ruta);
            var response = _mapper.Map<ArchivoDtoReponse>(archivoDb);
            response.ArchivoBase64 = Convert.ToBase64String(fileBytes);

            return response;
        }

        public async Task<bool> DeleteArchivoAsync(int idArchivo)
        {
            var archivo = await _repo.GetByIdAsync(idArchivo);
            if (archivo == null)
            {
                return false;
            }

            if (System.IO.File.Exists(archivo.Ruta))
            {
                System.IO.File.Delete(archivo.Ruta);

                // Quitar el nombre del archivo del final de la ruta
                var directory = Path.GetDirectoryName(archivo.Ruta.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                if (!string.IsNullOrEmpty(directory) && Directory.Exists(directory))
                {
                    // Solo elimina si la carpeta está vacía
                    if (!Directory.EnumerateFileSystemEntries(directory).Any())
                    {
                        Directory.Delete(directory);
                    }
                }
            }
            await _repo.DeleteAsync(archivo); // Actualiza el estado en la DB

            return true;
        }
    }
}
