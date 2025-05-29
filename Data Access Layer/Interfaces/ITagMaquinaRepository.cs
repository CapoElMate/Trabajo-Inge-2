using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;

namespace Data_Access_Layer.Interfaces
{
    public interface ITagMaquinaRepository
    {
        Task<IEnumerable<TagMaquina>> GetAllAsync();
        Task<TagMaquina?> GetByNameAsync(string tagMaquina);
        Task AddAsync(TagMaquina tagMaquina);
        Task UpdateAsync(TagMaquina tagMaquina);
        Task DeleteAsync(TagMaquina tagMaquina);
        Task<bool> ExistsAsync(string tagMaquina);
    }
}
