using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repositorios.SQL
{

    public class PermisoEspecialRepository : IPermisoEspecialRepository
    {
        private readonly ApplicationDbContext _context;

        public PermisoEspecialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermisoEspecial>> GetAllAsync()
        {
            return await _context.PermisosEspeciales.ToListAsync();
        }

        public async Task<PermisoEspecial?> GetByNameAsync(string permisoEspecial)
        {
            return await _context.PermisosEspeciales.FirstOrDefaultAsync(m => m.Permiso == permisoEspecial);
        }

        public async Task AddAsync(PermisoEspecial permisoEspecial)
        {
            await _context.PermisosEspeciales.AddAsync(permisoEspecial);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PermisoEspecial permisoEspecial)
        {
            _context.PermisosEspeciales.Update(permisoEspecial);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PermisoEspecial permisoEspecial)
        {
            _context.PermisosEspeciales.Remove(permisoEspecial);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string permisoEspecial)
        {
            return await _context.PermisosEspeciales.AnyAsync(m => m.Permiso == permisoEspecial);
        }
    }
}
