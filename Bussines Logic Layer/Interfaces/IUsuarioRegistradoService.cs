using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs.Usuarios;

namespace Bussines_Logic_Layer.Interfaces
{
    public interface IUsuarioRegistradoService
    {
        Task<IEnumerable<UsuarioRegistradoDTO>> GetAllAsync();
        Task<UsuarioRegistradoDTO?> GetByDNIAsync(string dni);
        Task<UsuarioRegistradoDTO?> GetByEmailAsync(string email);
        Task<UsuarioRegistradoDTO> CreateAsync(UsuarioRegistradoDTO dto);
        Task<bool> UpdateAsync(string dni, UsuarioRegistradoDTO dto);
        Task<bool> DeleteByDNIAsync(string dni);
        Task<bool> DeleteByEmailAsync(string email);
    }
}
