using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs.Usuarios;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> GetAllAsync();
        Task<ClienteDto?> GetByDNIAsync(string dni);
        Task<ClienteDto?> GetByEmailAsync(string email);
        Task<ClienteDto> CreateAsync(ClienteDto dto);
        Task<bool> UpdateAsync(string dni, ClienteDto dto);
        Task<bool> ConfirmDNI(string dni);
        Task<bool> DeleteByDNIAsync(string dni);
        Task<bool> DeleteByEmailAsync(string email);
    }
}
