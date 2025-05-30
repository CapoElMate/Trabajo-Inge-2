using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IUsuarioRegistradoRepository
    {
        Task<IEnumerable<UsuarioRegistrado>> GetAllAsync();
        Task<UsuarioRegistrado?> GetByDNIAsync(string DNI);
        Task<UsuarioRegistrado?> GetByEmailAsync(string email);
        Task AddAsync(UsuarioRegistrado cliente);
        Task UpdateAsync(UsuarioRegistrado cliente);
        Task DeleteAsync(UsuarioRegistrado cliente);
        Task<bool> ExistsAsync(string DNI);
    }
}
