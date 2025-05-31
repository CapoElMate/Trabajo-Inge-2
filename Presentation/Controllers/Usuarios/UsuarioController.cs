using System.Net;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Bussines_Logic_Layer.Interfaces;
using Bussines_Logic_Layer.Managers;
using Mailjet.Client;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Usuarios
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRegistradoService _serviceUsuario;
        private readonly IEmails mailjetClient;
        public UsuarioController(IUsuarioRegistradoService serviceUsuario, IEmails mailjetClient)
        {
            _serviceUsuario = serviceUsuario;
            this.mailjetClient = mailjetClient;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UsuarioRegistradoDTO>>> GetUsuarios()
        {
            var usuarios = await _serviceUsuario.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("byDNI")]
        public async Task<ActionResult<UsuarioRegistradoDTO>> GetUsuarioByDNI(string DNI)
        {
            var usuario = await _serviceUsuario.GetByDNIAsync(DNI);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpGet("byEmail")]
        public async Task<ActionResult<UsuarioRegistradoDTO>> GetUsuarioByEmail(string email)
        {
            var usuario = await _serviceUsuario.GetByEmailAsync(email);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioRegistradoDTO>> PostUsuario(UsuarioRegistradoDTO dto)
        {
            var created = await _serviceUsuario.CreateAsync(dto);
            if (created == null)
                return BadRequest("El usuario no se encuentra en identity, utiliza /register antes de registrar el usuario.");
            //mailjetClient.SendRegisterConfirmation(created.Email, created.Nombre, created.Apellido);
            return CreatedAtAction(nameof(GetUsuarioByDNI), new { dni = created }, created);
        }

        [HttpPut]
        public async Task<IActionResult> PutUsuario(string dni, UsuarioRegistradoDTO dto)
        {
            var existingUsuario = await _serviceUsuario.GetByDNIAsync(dni);
            if (existingUsuario == null || dni != existingUsuario.DNI)
                return BadRequest("El usuario no existe.");

            var updated = await _serviceUsuario.UpdateAsync(dni, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("byDNI")]
        public async Task<IActionResult> DeleteByDNI(string dni)
        {
            var existingUsuario = await _serviceUsuario.GetByDNIAsync(dni);
            if (existingUsuario == null || dni != existingUsuario.DNI)
                return BadRequest("El usuario no existe.");

            var deleted = await _serviceUsuario.DeleteByDNIAsync(dni);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("byEmail")]
        public async Task<IActionResult> DeleteByEmail(string email)
        {
            var existingUsuario = await _serviceUsuario.GetByEmailAsync(email);
            if (existingUsuario == null || email != existingUsuario.Email)
                return BadRequest("El usuario no existe.");

            var deleted = await _serviceUsuario.DeleteByEmailAsync(email);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
