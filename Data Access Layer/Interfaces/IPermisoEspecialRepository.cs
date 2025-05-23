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
        Task<PermisoEspecial?> GetByNameAsync(string permisoEspecial);
        Task AddAsync(PermisoEspecial permisoEspecial);
        Task UpdateAsync(PermisoEspecial permisoEspecial);
        Task DeleteAsync(PermisoEspecial permisoEspecial);
        Task<bool> ExistsAsync(string permisoEspecial);
    }
}
