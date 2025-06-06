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
    public class TipoEntregaRepository : ITipoEntregaRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoEntregaRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TipoEntrega>> GetAllAsync()
        {
            return await _context.TiposEntrega
                .ToListAsync();
        }
        
        public async Task AddAsync(TipoEntrega TipoEntrega)
        {
            await _context.TiposEntrega.AddAsync(TipoEntrega);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(TipoEntrega TipoEntrega)
        {
            _context.TiposEntrega.Remove(TipoEntrega);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string tipoEntregaName)
        {
            return await _context.TiposEntrega.AnyAsync(u => u.Entrega == tipoEntregaName);
        }

        
        public async Task<TipoEntrega?> GetByIdAsync(string tipoEntregaName)
        {
            return await _context.TiposEntrega
                .FirstOrDefaultAsync(p => p.Entrega == tipoEntregaName);
        }

        public async Task UpdateAsync(TipoEntrega TipoEntrega)
        {
            _context.TiposEntrega.Update(TipoEntrega);
            await _context.SaveChangesAsync();
        }
    }
}
