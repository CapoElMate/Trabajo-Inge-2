using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.Publicacion;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IPublicacionService
    {
        Task<IEnumerable<PublicacionDto>> GetAllAsync();
        Task<PublicacionDto?> GetByIdAsync(int id);
        Task<PublicacionDto> CreateAsync(CreatePublicacionDto dto);
        Task<bool> UpdateAsync(PublicacionDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> LogicDeleteAsync(int id);
    }
}
