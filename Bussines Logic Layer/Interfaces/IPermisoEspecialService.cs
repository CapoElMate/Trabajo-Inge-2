using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Usuarios;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IPermisoEspecialService
    {
        Task<IEnumerable<PermisoEspecialDto>> GetAllAsync();
        Task<IEnumerable<PermisoEspecialUsuarioDto>> GetAllUsuariosAsync();
        Task<PermisoEspecialDto?> GetByNameAsync(string Permiso);
        Task<PermisoEspecialDto> CreateAsync(PermisoEspecialDto dto);
        Task<bool> UpdateAsync(PermisoEspecialDto dto);
        Task<bool> DeleteAsync(string Permiso);
        Task<ICollection<PermisoEspecialUsuarioDto>> GetByUserAsync(string dni);
        Task<bool> actualizarPermisoAsync(PermisoEspecialUsuarioDto dto);
        Task<bool> confirmPermisoEspecial(string dni, string permisoEspecial);
        Task<bool> rechazarPermisoEspecial(string dni, string permisoEspecial);
        Task<bool> borrarPermisoUsuarioAsync(PermisoEspecialUsuarioDto permiso);
        Task<PermisoEspecialUsuarioDto> AgregarPermisoEspecialUsuarioAsync(PermisoEspecialUsuarioDto dto);
    }
}
