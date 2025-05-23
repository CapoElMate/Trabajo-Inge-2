using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{

    public class TagMaquinaRepository : ITagMaquinaRepository
    {
        private readonly ApplicationDbContext _context;

        public TagMaquinaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagMaquina>> GetAllAsync()
        {
            return await _context.TagsMaquina.ToListAsync();
        }

        public async Task<TagMaquina?> GetByNameAsync(string tagMaquina)
        {
            return await _context.TagsMaquina.FirstOrDefaultAsync(m => m.Tag == tagMaquina);
        }

        public async Task AddAsync(TagMaquina tagMaquina)
        {
            await _context.TagsMaquina.AddAsync(tagMaquina);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TagMaquina tagMaquina)
        {
            _context.TagsMaquina.Update(tagMaquina);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TagMaquina tagMaquina)
        {
            _context.TagsMaquina.Remove(tagMaquina);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string tagMaquina)
        {
            return await _context.TagsMaquina.AnyAsync(m => m.Tag == tagMaquina);
        }
    }
}
