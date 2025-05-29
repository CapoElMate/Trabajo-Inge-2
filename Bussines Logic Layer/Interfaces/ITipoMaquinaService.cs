using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface ITipoMaquinaService
    {
        Task<IEnumerable<TipoMaquinaDto>> GetAllAsync();
        Task<TipoMaquinaDto?> GetByNameAsync(string Tipo);
        Task<TipoMaquinaDto> CreateAsync(TipoMaquinaDto dto);
        Task<bool> UpdateAsync(TipoMaquinaDto dto);
        Task<bool> DeleteAsync(string Tipo);
    }
}
