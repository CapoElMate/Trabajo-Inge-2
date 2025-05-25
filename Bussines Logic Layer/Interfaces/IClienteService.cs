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
        Task<IEnumerable<ClienteDTO>> GetAllAsync();
        Task<ClienteDTO?> GetByDNIAsync(string dni);
        Task<ClienteDTO?> GetByEmailAsync(string email);
        Task<ClienteDTO> CreateAsync(ClienteDTO dto);
        Task<bool> UpdateAsync(string dni, ClienteDTO dto);
        Task<bool> DeleteByDNIAsync(string dni);
        Task<bool> DeleteByEmailAsync(string email);
    }
}
