using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IInfoAsentadaRepository
    {
        Task<IEnumerable<InfoAsentada>> GetAllAsync();
        Task<InfoAsentada?> GetByIdAsync(int id);
        Task AddAsync(InfoAsentada infoAsentada);
        Task UpdateAsync(InfoAsentada infoAsentada);
        Task DeleteAsync(InfoAsentada infoAsentada);
        Task<bool> ExistsAsync(int id);
    }
}
