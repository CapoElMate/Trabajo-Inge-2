using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs.Usuarios;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoDTO>> GetAllAsync();
        Task<EmpleadoDTO?> GetByDNIAsync(string dni);
        Task<EmpleadoDTO?> GetByEmailAsync(string email);
        Task<EmpleadoDTO> CreateAsync(EmpleadoDTO dto);
        Task<bool> UpdateAsync(string dni, EmpleadoDTO dto);
        Task<bool> DeleteByDNIAsync(string dni);
        Task<bool> DeleteByEmailAsync(string email);
        Task<EmpleadoDTO?> GetByNroEmpleadoAsync(int nroEmpleado);
        Task<bool> DeleteByNroEmpleadoAsync(int nroEmpleado);
    }
}
