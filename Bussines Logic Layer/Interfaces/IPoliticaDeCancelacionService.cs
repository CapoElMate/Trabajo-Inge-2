using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IPoliticaDeCancelacionService
    {
        Task<IEnumerable<PoliticaDeCancelacionDto>> GetAllAsync();
        Task<PoliticaDeCancelacionDto?> GetByNameAsync(string politica);
        Task<PoliticaDeCancelacionDto> CreateAsync(PoliticaDeCancelacionDto dto);
        Task<bool> UpdateAsync(string politica, PoliticaDeCancelacionDto dto);
        Task<bool> DeleteAsync(string politica);

    }
}
