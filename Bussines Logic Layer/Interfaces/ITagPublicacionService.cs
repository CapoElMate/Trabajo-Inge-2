using Bussines_Logic_Layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface ITagPublicacionService
    {
        Task<IEnumerable<TagPublicacionDto>> GetAllAsync();
        Task<TagPublicacionDto?> GetByNameAsync(string tagPublicacion);
        Task<TagPublicacionDto> CreateAsync(TagPublicacionDto dto);
        Task<bool> UpdateAsync(TagPublicacionDto dto);
        Task<bool> DeleteAsync(string tagPublicacion);
    }
}
