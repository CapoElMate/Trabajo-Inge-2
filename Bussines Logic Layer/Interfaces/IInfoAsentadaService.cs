using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.InfoAsentada;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IInfoAsentadaService
    {
        Task<IEnumerable<InfoAsentadaDto>> GetAllAsync();
        Task<InfoAsentadaDto?> GetByIdAsync(int id);
        Task<InfoAsentadaDto> CreateAsync(CreateInfoAsentadaDto dto);
        Task<bool> UpdateAsync(InfoAsentadaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
