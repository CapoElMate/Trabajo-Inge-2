using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IPermisoEspecialService
    {
        Task<IEnumerable<PermisoEspecialDto>> GetAllAsync();
        Task<PermisoEspecialDto?> GetByNameAsync(string Permiso);
        Task<PermisoEspecialDto> CreateAsync(PermisoEspecialDto dto);
        Task<bool> UpdateAsync(PermisoEspecialDto dto);
        Task<bool> DeleteAsync(string Permiso);
    }
}
