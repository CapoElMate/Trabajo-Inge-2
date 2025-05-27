using System.Net;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Usuarios
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _serviceEmpledo;

        public EmpleadoController(IEmpleadoService serviceEmpleado)
        {
            _serviceEmpledo = serviceEmpleado;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>> GetEmpleados()
        {
            var usuarios = await _serviceEmpledo.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("byNroEmpleado")]
        public async Task<ActionResult<EmpleadoDto>> GetUsuarioByNroEmpleado(int nroEmpleado)
        {
            var usuario = await _serviceEmpledo.GetByNroEmpleadoAsync(nroEmpleado);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpDelete("byNroEmpleado")]
        public async Task<IActionResult> DeleteByNroEmpleado(int nroEmpleado)
        {
            var existingEmpleado = await _serviceEmpledo.GetByNroEmpleadoAsync(nroEmpleado);
            if (existingEmpleado == null || nroEmpleado != existingEmpleado.nroEmpleado)
                return BadRequest("El empelado no existe.");

            var deleted = await _serviceEmpledo.DeleteByNroEmpleadoAsync(nroEmpleado);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("byDNI")]
        public async Task<ActionResult<EmpleadoDto>> GetEmpleadoByDNI(string DNI)
        {
            var usuario = await _serviceEmpledo.GetByDNIAsync(DNI);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpGet("byEmail")]
        public async Task<ActionResult<EmpleadoDto>> GetEmpleadoByEmail(string email)
        {
            var usuario = await _serviceEmpledo.GetByEmailAsync(email);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoDto>> PostEmpleado(EmpleadoDto dto)
        {
            var created = await _serviceEmpledo.CreateAsync(dto);
            return CreatedAtAction(nameof(GetEmpleadoByDNI), new { dni = created }, created);
        }

        //[HttpPut]
        //public async Task<IActionResult> PutEmpleado(string dni, EmpleadoDto dto)
        //{
        //    var existingEmpleado = await _serviceEmpledo.GetByDNIAsync(dni);
        //    if (existingEmpleado == null || dni != existingEmpleado.Cliente.UsuarioRegistrado.DNI)
        //        return BadRequest("El empelado no existe.");

        //    var updated = await _serviceEmpledo.UpdateAsync(dni, dto);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete("byDNI")]
        public async Task<IActionResult> DeleteByDNI(string dni)
        {
            var existingEmpleado = await _serviceEmpledo.GetByDNIAsync(dni);
            if (existingEmpleado == null || dni != existingEmpleado.Cliente.UsuarioRegistrado.DNI)
                return BadRequest("El empelado no existe.");

            var deleted = await _serviceEmpledo.DeleteByDNIAsync(dni);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("byEmail")]
        public async Task<IActionResult> DeleteByEmail(string email)
        {
            var existingEmpleado = await _serviceEmpledo.GetByEmailAsync(email);
            if (existingEmpleado == null || email != existingEmpleado.Cliente.UsuarioRegistrado.Email)
                return BadRequest("El empelado no existe.");

            var deleted = await _serviceEmpledo.DeleteByEmailAsync(email);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
