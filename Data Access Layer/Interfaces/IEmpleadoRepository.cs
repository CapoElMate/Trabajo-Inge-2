using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> GetAllAsync();
        Task<Empleado?> GetByDNIAsync(string DNI);
        Task<Empleado?> GetByEmailAsync(string email);
        Task<Empleado?> GetByNroEmpleadoAsync(int nroEmpleado);
        Task AddAsync(Empleado empleado);
        Task UpdateAsync(Empleado empleado);
        Task DeleteAsync(Empleado empleado);
        Task<bool> ExistsAsync(string DNI);
    }
}
