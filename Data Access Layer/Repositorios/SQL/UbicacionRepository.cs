using Data_Access_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositorios.SQL
{
    public class UbicacionRepository : IUbicacionRepository
    {
        private readonly ApplicationDbContext _context;

        public UbicacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Ubicacion>> GetAllAsync()
        {
            return await _context.Ubicaciones
                .ToListAsync();
        }
        
        public async Task AddAsync(Ubicacion Ubicacion)
        {
            await _context.Ubicaciones.AddAsync(Ubicacion);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Ubicacion Ubicacion)
        {
            _context.Ubicaciones.Remove(Ubicacion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string ubicacionName)
        {
            return await _context.Ubicaciones.AnyAsync(u => u.UbicacionName == ubicacionName);
        }

        
        public async Task<Ubicacion?> GetByIdAsync(string ubicacionName)
        {
            return await _context.Ubicaciones
                .FirstOrDefaultAsync(p => p.UbicacionName == ubicacionName);
        }

        public async Task UpdateAsync(Ubicacion Ubicacion)
        {
            _context.Ubicaciones.Update(Ubicacion);
            await _context.SaveChangesAsync();
        }
    }
}
