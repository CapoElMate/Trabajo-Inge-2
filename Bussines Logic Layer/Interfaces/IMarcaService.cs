using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaDto>> GetAllAsync();
        Task<MarcaDto?> GetByNameAsync(string Marca);
        Task<MarcaDto> CreateAsync(MarcaDto dto);
        Task<bool> UpdateAsync(MarcaDto dto);
        Task<bool> DeleteAsync(string Marca);
    }
}
