using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IPermisoEspecialRepository
    {
        Task<IEnumerable<PermisoEspecial>> GetAllAsync();
        Task<IEnumerable<UsuarioRegistrado_PermisoEspecial>> GetAllUsuariosAsync();
        
        Task<PermisoEspecial?> GetByNameAsync(string permisoEspecial);
        Task<ICollection<UsuarioRegistrado_PermisoEspecial>> GetByUserAsync(string dni);
        Task AddAsync(PermisoEspecial permisoEspecial);
        Task UpdateAsync(PermisoEspecial permisoEspecial);
        Task DeleteAsync(PermisoEspecial permisoEspecial);
        Task<bool> ExistsAsync(string permisoEspecial);
        Task ActualizarPermisoUsuarioAsync(UsuarioRegistrado_PermisoEspecial permiso);
        Task BorrarPermisoUsuarioAsync(UsuarioRegistrado_PermisoEspecial permiso);
        Task AgregarPermisoUsuarioAsync(UsuarioRegistrado_PermisoEspecial permiso);
    }
}
