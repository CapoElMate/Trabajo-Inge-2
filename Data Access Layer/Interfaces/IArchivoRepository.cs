using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;
using Domain_Layer.ValueObjects;

namespace Data_Access_Layer.Interfaces
{
    public interface IArchivoRepository
    {
        Task<Archivo?> GetByIdAsync(int id);
        Task<IEnumerable<Archivo>> GetByEntidadAsync(int entidadId, TipoEntidadArchivo tipoEntidad);
        Task AddAsync(Archivo archivo);
        Task UpdateAsync(Archivo archivo);
        Task DeleteAsync(Archivo archivo); // Para eliminación física si la manejas
        // Si usas borrado lógico, el UpdateAsync ya es suficiente para el cambio de estado
    }
}
