using System.Net;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PermisosEspecialesController : ControllerBase
    {
        private readonly IPermisoEspecialService _service;

        public PermisosEspecialesController(IPermisoEspecialService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PermisoEspecialDto>>> GetPermisosEspeciales()
        {
            var permisosEspeciales = await _service.GetAllAsync();
            return Ok(permisosEspeciales);
        }

        [HttpGet("byUser")]
        public async Task<ActionResult<ICollection<PermisoEspecialUsuarioDto>>> GetPermisosEspecialesPorUsuario(string dni)
        {
            var permisosEspeciales = await _service.GetByUserAsync(dni);
            return Ok(permisosEspeciales);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<PermisoEspecialDto>> GetPermisoEspecial(string permiso)
        {
            var permisoEspecial = await _service.GetByNameAsync(permiso);
            if (permisoEspecial == null)
                return NotFound();

            return Ok(permisoEspecial);
        }

        [HttpPost("crearPermiso")]
        public async Task<ActionResult<PermisoEspecialDto>> PostPermisoEspecial(PermisoEspecialDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetPermisoEspecial), new { Permiso = created }, created);
        }
        [HttpPost("agregarPermisoUsuario")]
        public async Task<ActionResult<PermisoEspecialUsuarioDto>> AgregarPermisoUsuario(PermisoEspecialUsuarioDto dto)
        {
            var created = await _service.AgregarPermisoEspecialUsuarioAsync(dto);
            return CreatedAtAction(nameof(GetPermisosEspecialesPorUsuario), new { Permiso = created }, created);
        }

        //[HttpPut()]
        //public async Task<IActionResult> PutMaquina(string permiso, PermisoEspecialDto dto)
        //{
        //    var permisoE = await _service.GetByNameAsync(permiso);
        //    if (permisoE == null || !permiso.Equals(permisoE.Permiso))
        //        return BadRequest("El permiso no existe.");

        //    var updated = await _service.UpdateAsync(dto);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}
        [HttpPut("actualizarPermisoUsuario")]
        public async Task<IActionResult> actualizarPermisoUsuario(PermisoEspecialUsuarioDto dto)
        {
            var permisos = await _service.GetByUserAsync(dto.DNICliente);
            if (permisos == null || !permisos.Any(p => p.Permiso.Equals(dto.Permiso)))
                return BadRequest("El permiso no existe.");

            var updated = await _service.actualizarPermisoAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("borrarPermiso")]
        public async Task<IActionResult> DeletePermiso(string permiso)
        {
            var permisoE = await _service.GetByNameAsync(permiso);
            if (permisoE == null || !permiso.Equals(permisoE.Permiso))
                return BadRequest("El permiso no existe.");

            var deleted = await _service.DeleteAsync(permiso);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("borrarPermisoUsuario")]
        public async Task<IActionResult> borrarPermisoEspecialUsuario(string dni, string permiso)
        {
            var permisos = await _service.GetByUserAsync(dni);
            if (permisos == null || !permisos.Any(p => p.Permiso.Equals(permiso)))
                return BadRequest("El permiso no existe.");

            var deleted = await _service.borrarPermisoUsuarioAsync(permisos.FirstOrDefault(p => p.Permiso.Equals(permiso)));
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
