using Bussines_Logic_Layer.DTOs.Archivo;
using Bussines_Logic_Layer.Interfaces;
using Domain_Layer.Entidades;
using Domain_Layer.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivoService _service;
        private readonly IWebHostEnvironment _hostingEnvironment; // Inyectado para acceso a wwwroot

        public ArchivoController(IArchivoService service, IWebHostEnvironment hostingEnvironment)
        {
            _service = service;
            _hostingEnvironment = hostingEnvironment;
        }

        // POST: Sube un nuevo archivo junto con sus metadatos.
        // Ejemplo de uso: POST /api/Archivos
        [HttpPost]
        [Consumes("multipart/form-data")] // Indica que este endpoint espera datos de formulario multipart
        public async Task<ActionResult<ArchivoDtoUpload>> UploadArchivo([FromForm] ArchivoDtoUpload dto)
        {
            try
            {
                // El servicio maneja las validaciones del archivo (tipo, tamaño) y el guardado físico.
                var createdArchivo = await _service.UploadFileAsync(dto);

                if (createdArchivo == null)
                {
                    // Esto puede ocurrir si el archivo en el DTO es nulo o vacío
                    return BadRequest("No se pudo procesar la solicitud de subida de archivo. El archivo no fue proporcionado o está vacío.");
                }

                // Retorna 201 Created (Creado) con la ubicación del nuevo recurso.
                // Usamos nameof(GetArchivoById) para que la respuesta incluya un enlace a cómo obtener el archivo.
                return CreatedAtAction(nameof(GetArchivoById), new { idArchivo = createdArchivo.Nombre }, createdArchivo);
            }
            catch (ArgumentException ex)
            {
                // Captura excepciones de validación específicas (ej. tipo o tamaño de archivo no permitido)
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada y devuelve un error 500.
                return StatusCode(500, $"Error interno del servidor al subir el archivo: {ex.Message}");
            }
        }

        [HttpPut("update")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateArchivo([FromForm] ArchivoDtoUpdate dto)
        {
            try
            {
                var updatedArchivo = await _service.UpdateArchivoAsync(dto);

                if (updatedArchivo == null)
                    return NotFound($"Archivo con ID {dto.IdArchivo} no encontrado.");

                return Ok(updatedArchivo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al actualizar archivo: {ex.Message}");
            }
        }

        // GET: Descarga un archivo específico por su IdArchivo.
        // Ejemplo de uso: GET /api/Archivos/download/5
        [HttpGet("download")]
        public async Task<IActionResult> GetArchivoById(int idArchivo)
        {
            try
            {
                var archivoDb = await _service.GetFileByIdAsync(idArchivo);

                if (archivoDb == null)
                {
                    return NotFound($"Archivo con ID {idArchivo} no encontrado o inactivo.");
                }

                var basePath = AppContext.BaseDirectory;
                var directoryInfo = new DirectoryInfo(basePath);
                var filePath = Path.Combine(directoryInfo.FullName.Split("bin")[0], archivoDb.Ruta.TrimStart('/'));

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound($"El archivo físico asociado al ID {idArchivo} no fue encontrado en el servidor.");
                }
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileBytes, archivoDb.TipoContenido, archivoDb.Nombre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al intentar descargar el archivo: {ex.Message}");
            }
        }

        // GET: Obtiene una lista de metadatos de archivos asociados a una entidad específica.
        [HttpGet("byEntidad")]
        public async Task<ActionResult<IEnumerable<ArchivoDtoReponse>>> GetArchivosByEntidad(
            int entidadId,
            TipoEntidadArchivo tipoEntidad) // ASP.NET Core mapea automáticamente el string del path a tu enum
        {
            try
            {
                var archivos = await _service.GetFilesByEntidadAsync(entidadId, tipoEntidad);

                if (archivos == null || !archivos.Any())
                {
                    return NotFound($"No se encontraron archivos asociados a EntidadID: {entidadId} y TipoEntidad: {tipoEntidad}.");
                }

                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al obtener la lista de archivos por entidad: {ex.Message}");
            }
        }

        // GET: Obtiene una lista de metadatos de archivos asociados a una entidad específica.
        [HttpGet("User/PermisosEspeciales")]
        public async Task<ActionResult<IEnumerable<ArchivoDtoReponse>>> GetPermisosEspecialesUser(string DNI)
        {
            try
            {
                var archivos = await _service.GetFilesByEntidadAsync(Int32.Parse(DNI), TipoEntidadArchivo.PermisoEspecial);

                if (archivos == null || !archivos.Any())
                {
                    return NotFound($"No se encontraron permisos especiales asociados al usuario: {DNI}.");
                }

                return Ok(archivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al obtener la lista de archivos por entidad: {ex.Message}");
            }
        }

        // GET: Obtiene una lista de metadatos de archivos asociados a una entidad específica.
        [HttpGet("User/DNI")]
        public async Task<ActionResult<ArchivoDtoReponse>> GetDNI(string DNI)
        {
            try
            {
                var archivos = await _service.GetFilesByEntidadAsync(Int32.Parse(DNI), TipoEntidadArchivo.DNI);

                if (archivos == null || !archivos.Any())
                {
                    return NotFound($"No se encontraron permisos especiales asociados al usuario: {DNI}.");
                }

                return Ok(archivos.First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al obtener la lista de archivos por entidad: {ex.Message}");
            }
        }

        // DELETE: Elimina un archivo por su IdArchivo (puede ser lógico o físico según la implementación del servicio).
        // Ejemplo de uso: DELETE /api/Archivos/5
        [HttpDelete("byId")]
        public async Task<IActionResult> DeleteArchivo(int idArchivo)
        {
            try
            {
                var deleted = await _service.DeleteArchivoAsync(idArchivo);

                if (!deleted)
                {
                    // Si el servicio devuelve 'false', el archivo no existía o no pudo ser eliminado.
                    return NotFound($"No se pudo eliminar el archivo con ID {idArchivo}. Podría no existir o ya estar inactivo.");
                }

                // Retorna 204 No Content para indicar una eliminación exitosa sin contenido de respuesta.
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al eliminar el archivo: {ex.Message}");
            }
        }
    }
}
