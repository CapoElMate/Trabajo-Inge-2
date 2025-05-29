using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{

    public class MaquinaRepository : IMaquinaRepository
    {
        private readonly ApplicationDbContext _context;

        public MaquinaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Maquina>> GetAllAsync()
        {
            return await _context.Maquinas
                .Include(m => m.Modelo)
                .Include(m => m.PermisosEspeciales)
                .Include(m => m.Modelo.Marca)
                .Include(m => m.TipoMaquina)
                .Include(m => m.TagsMaquina) 
                .ToListAsync();
        }

        public async Task<Maquina?> GetByIdAsync(int id)
        {
            return await _context.Maquinas
                .Include(m => m.Modelo)
                .Include(m => m.PermisosEspeciales)
                .Include(m => m.Modelo.Marca)
                .Include(m => m.TipoMaquina)
                .Include(m => m.TagsMaquina)
                .FirstOrDefaultAsync(m => m.idMaquina == id);
        }

        public async Task AddAsync(Maquina maquina)
        {
            await _context.Maquinas.AddAsync(maquina);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Maquina maquina)
        {
            _context.Maquinas.Update(maquina);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Maquina maquina)
        {
            _context.Maquinas.Remove(maquina);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Maquinas.AnyAsync(m => m.idMaquina == id);
        }
    }
}
