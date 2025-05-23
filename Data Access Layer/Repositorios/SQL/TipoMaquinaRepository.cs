using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{

    public class TipoMaquinaRepository : ITipoMaquinaRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoMaquinaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoMaquina>> GetAllAsync()
        {
            return await _context.TiposMaquina.ToListAsync();
        }

        public async Task<TipoMaquina?> GetByNameAsync(string tipoMaquina)
        {
            return await _context.TiposMaquina.FirstOrDefaultAsync(m => m.Tipo == tipoMaquina);
        }

        public async Task AddAsync(TipoMaquina tipoMaquina)
        {
            await _context.TiposMaquina.AddAsync(tipoMaquina);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TipoMaquina tipoMaquina)
        {
            _context.TiposMaquina.Update(tipoMaquina);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TipoMaquina tipoMaquina)
        {
            _context.TiposMaquina.Remove(tipoMaquina);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string tipoMaquina)
        {
            return await _context.TiposMaquina.AnyAsync(m => m.Tipo == tipoMaquina);
        }
    }
}
