using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IReembolsoRepository
    {
        Task<IEnumerable<Reembolso>> GetAllAsync();
        Task<Reembolso?> GetByIdAsync(int id);
        Task AddAsync(Reembolso reembolso);
        Task UpdateAsync(Reembolso reembolso);
        Task DeleteAsync(Reembolso reembolso);
        Task<bool> ExistsAsync(int id);
    }
}
