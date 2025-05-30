using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IUbicacionService
    {
        Task<IEnumerable<UbicacionDto>> GetAllAsync();
        Task<UbicacionDto?> GetByIdAsync(string ubicacionName);
        Task<UbicacionDto> CreateAsync(UbicacionDto dto);
        Task<bool> UpdateAsync(UbicacionDto dto);
        Task<bool> DeleteAsync(string ubicacionName);
    }
}
