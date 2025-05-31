using System.Net;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Bussines_Logic_Layer.Interfaces;
using Mailjet.Client;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Usuarios
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _serviceCliente;
        private readonly IEmails _mailjetClient;
        public ClienteController(IClienteService serviceCliente, IEmails mailjetClient)
        {
            _serviceCliente = serviceCliente;
            _mailjetClient = mailjetClient;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetUsuarios()
        {
            var usuarios = await _serviceCliente.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("byDNI")]
        public async Task<ActionResult<ClienteDto>> GetUsuarioByDNI(string DNI)
        {
            var usuario = await _serviceCliente.GetByDNIAsync(DNI);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpGet("isValidateDNI")]
        public async Task<ActionResult<UsuarioRegistradoDTO>> IsValidateDNI(string DNI)
        {
            var usuario = await _serviceCliente.GetByDNIAsync(DNI);
            if (usuario == null)
                return NotFound();

            return Ok(usuario.UsuarioRegistrado.dniVerificado);
        }

        [HttpGet("byEmail")]
        public async Task<ActionResult<ClienteDto>> GetUsuarioByEmail(string email)
        {
            var usuario = await _serviceCliente.GetByEmailAsync(email);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> PostCliente(ClienteDto dto)
        {
            var created = await _serviceCliente.CreateAsync(dto);
            return CreatedAtAction(nameof(GetUsuarioByDNI), new { dni = created }, created);
        }

        [HttpPut("ConfirmDNI")]
        public async Task<IActionResult> ValidarDNI(string dni)
        {
            //var cliente = await _serviceCliente.GetByDNIAsync(dni);
            //if (cliente == null || dni != cliente.UsuarioRegistrado.DNI)
            //    return BadRequest("El cliente no existe.");

            var updated = await _serviceCliente.ConfirmDNI(dni);
            if (!updated)
                return BadRequest("No se pudo validar el DNI.");
            //_mailjetClient.SendDNIValidationConfirmation(cliente.UsuarioRegistrado.Email, cliente.UsuarioRegistrado.Nombre, cliente.UsuarioRegistrado.Apellido);
            return Ok("DNI Validado correctamente.");
        }
        //[HttpPut]
        //public async Task<IActionResult> PutMaquina(string dni, ClienteDto dto)
        //{
        //    var cliente = await _serviceCliente.GetByDNIAsync(dni);
        //    if (cliente == null || dni != cliente.UsuarioRegistrado.DNI)
        //        return BadRequest("El cliente no existe.");

        //    var updated = await _serviceCliente.UpdateAsync(dni, dto);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete("byDNI")]
        public async Task<IActionResult> DeleteByDNI(string dni)
        {
            var cliente = await _serviceCliente.GetByDNIAsync(dni);
            if (cliente == null || dni != cliente.UsuarioRegistrado.DNI)
                return BadRequest("El cliente no existe.");

            var deleted = await _serviceCliente.DeleteByDNIAsync(dni);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("byEmail")]
        public async Task<IActionResult> DeleteByEmail(string email)
        {
            var cliente = await _serviceCliente.GetByEmailAsync(email);
            if (cliente == null || email != cliente.UsuarioRegistrado.Email)
                return BadRequest("El cliente no existe.");

            var deleted = await _serviceCliente.DeleteByEmailAsync(email);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
