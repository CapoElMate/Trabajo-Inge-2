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
        Task<IEnumerable<EmpleadoDto>> GetAllAsync();
        Task<EmpleadoDto?> GetByDNIAsync(string dni);
        Task<EmpleadoDto?> GetByEmailAsync(string email);
        Task<EmpleadoDto> CreateAsync(CreateEmpleadoDto dto);
        Task<bool> UpdateAsync(string dni, EmpleadoDto dto);
        Task<bool> DeleteByDNIAsync(string dni);
        Task<bool> DeleteByEmailAsync(string email);
        Task<EmpleadoDto?> GetByNroEmpleadoAsync(int nroEmpleado);
        Task<bool> DeleteByNroEmpleadoAsync(int nroEmpleado);
    }
}
