using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IUbicacionRepository
    {
        Task<IEnumerable<Ubicacion>> GetAllAsync();
        Task<Ubicacion?> GetByIdAsync(string id);
        Task AddAsync(Ubicacion ubicacion);
        Task UpdateAsync(Ubicacion ubicacion);
        Task DeleteAsync(Ubicacion ubicacion);
        Task<bool> ExistsAsync(string id);
    }
}
