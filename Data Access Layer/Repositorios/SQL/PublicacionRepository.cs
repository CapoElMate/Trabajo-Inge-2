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
    public class PublicacionRepository : IPublicacionRepository
    {
        private readonly ApplicationDbContext _context;

        public PublicacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Publicacion>> GetAllAsync()
        {
            return await _context.Publicaciones
                .Include(p => p.Maquina)
                .Include(p => p.TagsPublicacion)
                //.Include(p => p.Comentarios)
                .Include(p => p.PoliticaDeCancelacion)
                .Include(p => p.Ubicacion)
                .ToListAsync();
        }
        
        public async Task AddAsync(Publicacion publicacion)
        {
            await _context.Publicaciones.AddAsync(publicacion);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Publicacion publicacion)
        {
            _context.Publicaciones.Remove(publicacion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Publicaciones.AnyAsync(p => p.idPublicacion == id);
        }

        
        public async Task<Publicacion?> GetByIdAsync(int id)
        {
            return await _context.Publicaciones
                .Include(p => p.Maquina)
                .Include(p => p.TagsPublicacion)
                //.Include(p => p.Comentarios)
                .Include(p => p.PoliticaDeCancelacion)
                .Include(p => p.Ubicacion)
                .FirstOrDefaultAsync(p => p.idPublicacion == id);
        }

        public async Task UpdateAsync(Publicacion publicacion)
        {
            _context.Publicaciones.Update(publicacion);
            await _context.SaveChangesAsync();
        }
    }
}
