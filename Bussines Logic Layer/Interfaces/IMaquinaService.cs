using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IMaquinaService
    {
        Task<IEnumerable<MaquinaDto>> GetAllAsync();
        Task<MaquinaDto?> GetByIdAsync(int id);
        Task<MaquinaDto> CreateAsync(CreateMaquinaDto dto);
        Task<bool> UpdateAsync(MaquinaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
