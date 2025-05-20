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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermisoEspecialDto>>> GetPermisosEspeciales()
        {
            var permisosEspeciales = await _service.GetAllAsync();
            return Ok(permisosEspeciales);
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquina(string permiso, PermisoEspecialDto dto)
        {
            if (!permiso.Equals(dto.Permiso))
                return BadRequest();

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(string permiso)
        {
            var deleted = await _service.DeleteAsync(permiso);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
