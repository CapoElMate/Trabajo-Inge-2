using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{
    public class UsuarioRegistradoRepository : IUsuarioRegistradoRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRegistradoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioRegistrado>> GetAllAsync()
        {
            return await _context.UsuariosRegistrados
                .Include(u => u.PermisosEspeciales)
                .ToListAsync();
        }

        public async Task<UsuarioRegistrado?> GetByDNIAsync(string dni)
        {
            return await _context.UsuariosRegistrados
                .FirstOrDefaultAsync(u => u.DNI == dni);
        }

        public async Task<UsuarioRegistrado?> GetByEmailAsync(string email)
        {
            return await _context.UsuariosRegistrados
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(UsuarioRegistrado usuario)
        {
            await _context.UsuariosRegistrados.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UsuarioRegistrado usuario)
        {
            _context.UsuariosRegistrados.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UsuarioRegistrado usuario)
        {
            _context.UsuariosRegistrados.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string dni)
        {
            return await _context.UsuariosRegistrados.AnyAsync(u => u.DNI == dni);
        }
    }
}
