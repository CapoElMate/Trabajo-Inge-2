using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IModeloRepository
    {
        Task<IEnumerable<Modelo>> GetAllAsync();
        Task<Modelo?> GetByNameAsync(string modeloName);
        Task AddAsync(Modelo modelo);
        Task UpdateAsync(Modelo modelo);
        Task DeleteAsync(Modelo modelo);
        Task<bool> ExistsAsync(string modeloName);
    }
}
