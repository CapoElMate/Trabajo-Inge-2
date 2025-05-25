using Bussines_Logic_Layer.DTOs;
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

        [HttpGet("byName")]
        public async Task<ActionResult<PermisoEspecialDto>> GetPermisoEspecial(string permiso)
        {
            var permisoEspecial = await _service.GetByNameAsync(permiso);
            if (permisoEspecial == null)
                return NotFound();

            return Ok(permisoEspecial);
        }

        [HttpPost]
        public async Task<ActionResult<PermisoEspecialDto>> PostMaquina(PermisoEspecialDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetPermisoEspecial), new { Permiso = created }, created);
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

        [HttpDelete()]
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
    }
}
