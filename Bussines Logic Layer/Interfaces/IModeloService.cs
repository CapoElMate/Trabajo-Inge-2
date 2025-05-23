using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IModeloService
    {
        Task<IEnumerable<ModeloDto>> GetAllAsync();
        Task<ModeloDto?> GetByNameAsync(string Tipo);
        Task<ModeloDto> CreateAsync(ModeloDto dto);
        Task<bool> UpdateAsync(ModeloDto dto);
        Task<bool> DeleteAsync(string Tipo);
    }
}
