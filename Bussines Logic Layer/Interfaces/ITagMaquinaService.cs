using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface ITagMaquinaService
    {
        Task<IEnumerable<TagMaquinaDto>> GetAllAsync();
        Task<TagMaquinaDto?> GetByNameAsync(string tagMaquina);
        Task<TagMaquinaDto> CreateAsync(TagMaquinaDto dto);
        Task<bool> UpdateAsync(TagMaquinaDto dto);
        Task<bool> DeleteAsync(string tagMaquina);
    }
}