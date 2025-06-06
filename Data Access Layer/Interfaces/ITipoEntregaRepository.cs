using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface ITipoEntregaRepository
    {
        Task<IEnumerable<TipoEntrega>> GetAllAsync();
        Task<TipoEntrega?> GetByIdAsync(string entrega);
        Task AddAsync(TipoEntrega tipoEntrega);
        Task UpdateAsync(TipoEntrega tipoEntrega);
        Task DeleteAsync(TipoEntrega tipoEntrega);
        Task<bool> ExistsAsync(string entrega);
    }
}
