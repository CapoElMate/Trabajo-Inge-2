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
    public class ReembolsoRepository : IReembolsoRepository
    {
        private readonly ApplicationDbContext _context;

        public ReembolsoRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Reembolso>> GetAllAsync()
        {
            return await _context.Reembolsos
                .ToListAsync();
        }
        
        public async Task AddAsync(Reembolso Reembolso)
        {
            await _context.Reembolsos.AddAsync(Reembolso);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Reembolso Reembolso)
        {
            _context.Reembolsos.Remove(Reembolso);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reembolsos.AnyAsync(p => p.idReembolso == id);
        }

        
        public async Task<Reembolso?> GetByIdAsync(int id)
        {
            return await _context.Reembolsos
                .FirstOrDefaultAsync(p => p.idReembolso == id);
        }

        public async Task UpdateAsync(Reembolso Reembolso)
        {
            _context.Reembolsos.Update(Reembolso);
            await _context.SaveChangesAsync();
        }
    }
}
