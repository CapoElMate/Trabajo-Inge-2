using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{

    public class PoliticaDeCancelacionRepository : IPoliticaDeCancelacionRepository
    {
        private readonly ApplicationDbContext _context;

        public PoliticaDeCancelacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PoliticaDeCancelacion>> GetAllAsync()
        {
            return await _context.PoliticasDeCancelacion.ToListAsync();
        }

        public async Task<PoliticaDeCancelacion?> GetByNameAsync(string PoliticaDeCancelacion)
        {
            return await _context.PoliticasDeCancelacion.FirstOrDefaultAsync(m => m.Politica == PoliticaDeCancelacion);
        }

        public async Task AddAsync(PoliticaDeCancelacion PoliticaDeCancelacion)
        {
            await _context.PoliticasDeCancelacion.AddAsync(PoliticaDeCancelacion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PoliticaDeCancelacion PoliticaDeCancelacion)
        {
            _context.PoliticasDeCancelacion.Update(PoliticaDeCancelacion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PoliticaDeCancelacion PoliticaDeCancelacion)
        {
            _context.PoliticasDeCancelacion.Remove(PoliticaDeCancelacion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string PoliticaDeCancelacion)
        {
            return await _context.PoliticasDeCancelacion.AnyAsync(m => m.Politica == PoliticaDeCancelacion);
        }
    }
}
