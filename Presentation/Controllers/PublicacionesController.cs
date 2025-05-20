using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using Domain_Layer.Entidades;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PublicacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Publicacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publicacion>>> GetPublicaciones()
        {
            return await _context.Publicaciones.ToListAsync();
        }

        // GET: api/Publicacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publicacion>> GetPublicacion(int id)
        {
            var publicacion = await _context.Publicaciones.FindAsync(id);

            if (publicacion == null)
            {
                return NotFound();
            }

            return publicacion;
        }

        // PUT: api/Publicacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicacion(int id, Publicacion publicacion)
        {
            if (id != publicacion.idPublicacion)
            {
                return BadRequest();
            }

            _context.Entry(publicacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Publicacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publicacion>> PostPublicacion(Publicacion publicacion)
        {
            _context.Publicaciones.Add(publicacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublicacion", new { id = publicacion.idPublicacion }, publicacion);
        }

        // DELETE: api/Publicacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicacion(int id)
        {
            var publicacion = await _context.Publicaciones.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            _context.Publicaciones.Remove(publicacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublicacionExists(int id)
        {
            return _context.Publicaciones.Any(e => e.idPublicacion == id);
        }
    }
}
