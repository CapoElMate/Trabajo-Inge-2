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
    public class MaquinasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaquinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Maquinas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maquina>>> GetMaquinas()
        {
            return await _context.Maquinas.ToListAsync();
        }

        // GET: api/Maquinas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Maquina>> GetMaquina(int id)
        {
            var maquina = await _context.Maquinas.FindAsync(id);

            if (maquina == null)
            {
                return NotFound();
            }

            return maquina;
        }

        // PUT: api/Maquinas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquina(int id, Maquina maquina)
        {
            if (id != maquina.idMaquina)
            {
                return BadRequest();
            }

            _context.Entry(maquina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinaExists(id))
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

        // POST: api/Maquinas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Maquina>> PostMaquina(Maquina maquina)
        {
            _context.Maquinas.Add(maquina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaquina", new { id = maquina.idMaquina }, maquina);
        }

        // DELETE: api/Maquinas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(int id)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null)
            {
                return NotFound();
            }

            _context.Maquinas.Remove(maquina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaquinaExists(int id)
        {
            return _context.Maquinas.Any(e => e.idMaquina == id);
        }
    }
}
