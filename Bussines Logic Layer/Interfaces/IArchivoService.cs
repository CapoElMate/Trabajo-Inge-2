using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.Archivo;
using Domain_Layer.Entidades;
using Domain_Layer.ValueObjects;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IArchivoService
    {
        // Método para subir y guardar un archivo
        Task<ArchivoDtoUpload?> UploadFileAsync(ArchivoDtoUpload dto);

        // Método para obtener un archivo específico por su ID (para descargar)
        Task<Archivo?> GetFileByIdAsync(int idArchivo); // Retornar el modelo de DB para obtener la ruta y tipo

        // Método para obtener todos los archivos asociados a una entidad
        Task<IEnumerable<ArchivoDtoReponse>> GetFilesByEntidadAsync(int entidadId, TipoEntidadArchivo tipoEntidad);

        // Método para eliminar un archivo (lógica o física)
        Task<bool> DeleteArchivoAsync(int idArchivo);
    }
}
