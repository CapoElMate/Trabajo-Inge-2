using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Reembolso;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IReembolsoService
    {
        Task<IEnumerable<ReembolsoDto>> GetAllAsync();
        Task<ReembolsoDto?> GetByIdAsync(int id);
        Task<ReembolsoDto> CreateAsync(CreateReembolsoDto dto);
        Task<bool> UpdateAsync(ReembolsoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
