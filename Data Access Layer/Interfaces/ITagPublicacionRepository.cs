using Domain_Layer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface ITagPublicacionRepository
    {
        Task<IEnumerable<TagPublicacion>> GetAllAsync();
        Task<TagPublicacion?> GetByNameAsync(string tagPublicacion);
        Task AddAsync(TagPublicacion tagPublicacion);
        Task UpdateAsync(TagPublicacion tagPublicacion);
        Task DeleteAsync(TagPublicacion tagPublicacion);
        Task<bool> ExistsAsync(string tagPublicacion);
    }
}
