using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface IPublicacionRepository
    {
        Task<IEnumerable<Publicacion>> GetAllAsync();
        Task<Publicacion?> GetByIdAsync(int id);
        Task<Publicacion> AddAsync(Publicacion publicacion);
        Task UpdateAsync(Publicacion publicacion);
        Task DeleteAsync(Publicacion publicacion);
        Task<bool> ExistsAsync(int id);
    }
}
