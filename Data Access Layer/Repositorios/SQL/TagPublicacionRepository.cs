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
    public class TagPublicacionRepository:ITagPublicacionRepository
    {
        private readonly ApplicationDbContext _context;

        public TagPublicacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagPublicacion>> GetAllAsync()
        {
            return await _context.TagsPublicacion.ToListAsync();
        }

        public async Task<TagPublicacion?> GetByNameAsync(string tagPublicacion)
        {
            return await _context.TagsPublicacion.FirstOrDefaultAsync(m => m.Tag == tagPublicacion);
        }

        public async Task AddAsync(TagPublicacion tagPublicacion)
        {
            await _context.TagsPublicacion.AddAsync(tagPublicacion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TagPublicacion tagPublicacion)
        {
            _context.TagsPublicacion.Update(tagPublicacion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TagPublicacion tagPublicacion)
        {
            _context.TagsPublicacion.Remove(tagPublicacion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string tagPublicacion)
        {
            return await _context.TagsPublicacion.AnyAsync(m => m.Tag == tagPublicacion);
        }
    }
}
