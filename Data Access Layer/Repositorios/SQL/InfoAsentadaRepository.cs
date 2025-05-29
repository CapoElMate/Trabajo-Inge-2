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
    public class InfoAsentadaRepository : IInfoAsentadaRepository
    {
        private readonly ApplicationDbContext _context;

        public InfoAsentadaRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<InfoAsentada>> GetAllAsync()
        {
            return await _context.InfoAsentada
                .ToListAsync();
        }
        
        public async Task AddAsync(InfoAsentada InfoAsentada)
        {
            await _context.InfoAsentada.AddAsync(InfoAsentada);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(InfoAsentada InfoAsentada)
        {
            _context.InfoAsentada.Remove(InfoAsentada);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.InfoAsentada.AnyAsync(p => p.idInfo == id);
        }

        
        public async Task<InfoAsentada?> GetByIdAsync(int id)
        {
            return await _context.InfoAsentada
                .FirstOrDefaultAsync(p => p.idInfo == id);
        }

        public async Task UpdateAsync(InfoAsentada InfoAsentada)
        {
            _context.InfoAsentada.Update(InfoAsentada);
            await _context.SaveChangesAsync();
        }
    }
}
