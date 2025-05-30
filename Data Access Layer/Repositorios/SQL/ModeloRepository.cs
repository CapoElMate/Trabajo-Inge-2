using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{

    public class ModeloRepository : IModeloRepository
    {
        private readonly ApplicationDbContext _context;

        public ModeloRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Modelo>> GetAllAsync()
        {
            return await _context.Modelos
                .Include(m => m.Marca)
                .ToListAsync();
        }

        public async Task<Modelo?> GetByNameAsync(string modeloName)
        {
            return await _context.Modelos.FirstOrDefaultAsync(m => m.ModeloName == modeloName);
        }

        public async Task AddAsync(Modelo modelo)
        {
            await _context.Modelos.AddAsync(modelo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Modelo modelo)
        {
            _context.Modelos.Update(modelo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Modelo modelo)
        {
            _context.Modelos.Remove(modelo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string modeloName)
        {
            return await _context.Modelos.AnyAsync(m => m.ModeloName == modeloName);
        }
    }
}
