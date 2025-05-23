using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IMaquinaRepository
    {
        Task<IEnumerable<Maquina>> GetAllAsync();
        Task<Maquina?> GetByIdAsync(int id);
        Task AddAsync(Maquina maquina);
        Task UpdateAsync(Maquina maquina);
        Task DeleteAsync(Maquina maquina);
        Task<bool> ExistsAsync(int id);
    }
}
