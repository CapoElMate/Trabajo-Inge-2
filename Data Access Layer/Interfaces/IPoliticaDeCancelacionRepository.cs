using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IPoliticaDeCancelacionRepository
    {
        Task<IEnumerable<PoliticaDeCancelacion>> GetAllAsync();
        Task<PoliticaDeCancelacion?> GetByNameAsync(string politicaDeCancelacionName);
        Task AddAsync(PoliticaDeCancelacion politicaDeCancelacion);
        Task UpdateAsync(PoliticaDeCancelacion politicaDeCancelacion);
        Task DeleteAsync(PoliticaDeCancelacion politicaDeCancelacion);
        Task<bool> ExistsAsync(string politicaDeCancelacionName);
    }
}
