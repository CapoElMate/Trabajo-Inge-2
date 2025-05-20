using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using Microsoft.AspNetCore.Authorization;
using Bussines_Logic_Layer.Services;
using Microsoft.AspNetCore.Identity;
using API_Layer.DTOs;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRegistradoController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UsuarioRegistradoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/UsuarioRegistrado
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioRegistrado>>> GetUsuariosRegistrados()
        {
            return await _context.UsuariosRegistrados.ToListAsync();
        }

        // GET: api/UsuarioRegistrado/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioRegistrado>> GetUsuarioRegistrado(string id)
        {
            var usuarioRegistrado = await _context.UsuariosRegistrados.FindAsync(id);

            if (usuarioRegistrado == null)
            {
                return NotFound();
            }

            return usuarioRegistrado;
        }


        // GET: api/UsuarioRegistrado/mail/{id}
        [Authorize]
        [HttpGet("mail/{mail}")]
        public async Task<ActionResult<UsuarioRegistrado>> GetUsuarioRegistradoPorMail(string mail)
        {
            //busco asincronamente el primer usuario registrado con ese mail.
            var usuarioRegistrado = await _context.UsuariosRegistrados.FirstOrDefaultAsync(x => x.Email == mail);

            //si no lo encuentra tiro error
            if (usuarioRegistrado == null)
            {
                return NotFound();
            }

            //si lo encuentra retorna el usuarioRegistrado
            return usuarioRegistrado;
        }


        // PUT: api/UsuarioRegistrado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioRegistrado(string id, UsuarioRegistrado usuarioRegistrado)
        {
            if (id != usuarioRegistrado.DNI)
            {
                return BadRequest();
            }

            _context.Entry(usuarioRegistrado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioRegistradoExists(id))
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

        // POST: api/UsuarioRegistrado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost]
        public async Task<ActionResult<UsuarioRegistrado>> PostUsuarioRegistrado(UsuarioRegistrado usuarioRegistrado)
        {
            _context.UsuariosRegistrados.Add(usuarioRegistrado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioRegistradoExists(usuarioRegistrado.DNI))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetUsuarioRegistrado", new { id = usuarioRegistrado.DNI }, usuarioRegistrado);
        }

        // DELETE: api/UsuarioRegistrado/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioRegistrado(string id)
        {
            var usuarioRegistrado = await _context.UsuariosRegistrados.FindAsync(id);
            if (usuarioRegistrado == null)
            {
                return NotFound();
            }

            _context.UsuariosRegistrados.Remove(usuarioRegistrado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        private bool UsuarioRegistradoExists(string id)
        {
            return _context.UsuariosRegistrados.Any(e => e.DNI == id);
        }



        [HttpPost("registrarCompleto")]
        public async Task<IActionResult> RegistrarCompleto([FromBody] RegistroUsuarioCompletoDTO dto)
        {
            // 1. Registrar en Identity
            var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // 2. Registrar en UsuarioRegistrado
            var usuarioRegistrado = dto.getUsuarioRegistrado();

            await _context.UsuariosRegistrados.AddAsync(usuarioRegistrado);

            return Ok("Usuario registrado correctamente en ambos sistemas.");
        }



    }
}
