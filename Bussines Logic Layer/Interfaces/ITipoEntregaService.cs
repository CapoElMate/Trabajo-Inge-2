using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;
using Domain_Layer.Entidades;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface ITipoEntregaService
    {
        Task<IEnumerable<TipoEntregaDto>> GetAllAsync();
        Task<TipoEntregaDto?> GetByIdAsync(string tipo);
        Task<TipoEntregaDto> CreateAsync(TipoEntregaDto dto);
        Task<bool> UpdateAsync(TipoEntregaDto dto);
        Task<bool> DeleteAsync(string tipo);
    }
}
