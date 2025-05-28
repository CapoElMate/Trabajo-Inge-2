using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IAlquilerRepository
    {
        Task<IEnumerable<Alquiler>> GetAllAsync();
        Task<Alquiler?> GetByIdAsync(int id);
        Task AddAsync(Alquiler alquiler);
        Task UpdateAsync(Alquiler alquiler);
        Task DeleteAsync(Alquiler alquiler);
        Task<bool> ExistsAsync(int id);
    }
}
