using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{

    public class MarcaRepository : IMarcaRepository
    {
        private readonly ApplicationDbContext _context;

        public MarcaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marca>> GetAllAsync()
        {
            return await _context.Marcas.ToListAsync();
        }

        public async Task<Marca?> GetByNameAsync(string marcaName)
        {
            return await _context.Marcas.FirstOrDefaultAsync(m => m.MarcaName == marcaName);
        }

        public async Task AddAsync(Marca marca)
        {
            await _context.Marcas.AddAsync(marca);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Marca marca)
        {
            _context.Marcas.Update(marca);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Marca marca)
        {
            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string marcaName)
        {
            return await _context.Marcas.AnyAsync(m => m.MarcaName == marcaName);
        }
    }
}
