using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionController : Controller
    {
        private readonly IUbicacionService _service;

        public UbicacionController(IUbicacionService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UbicacionDto>>> GetUbicaciones()
        {
            var ubicaciones = await _service.GetAllAsync();
            return Ok(ubicaciones);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<UbicacionDto>> GetUbicacion(string ubicacionName)
        {
            var ubicacion = await _service.GetByIdAsync(ubicacionName);
            if (ubicacion == null)
                return NotFound();

            return Ok(ubicacion);
        }

        [HttpPost]
        public async Task<ActionResult<UbicacionDto>> PostUbicacion(UbicacionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetUbicacion), new { id = created }, created);
        }

        //[HttpPut("byName")]
        //public async Task<IActionResult> PutUbicacion(string ubicacionName, UbicacionDto dto)
        //{
        //    var ubicacion = await _service.GetByIdAsync(ubicacionName);
        //    if (ubicacion == null || !ubicacionName.Equals(ubicacion.UbicacionName))
        //        return BadRequest("La ubicacion no existe.");

        //    var updated = await _service.UpdateAsync(dto);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete("byName")]
        public async Task<IActionResult> DeleteUbicacion(string ubicacionName)
        {
            var ubicacion = await _service.GetByIdAsync(ubicacionName);
            if (ubicacion == null || !ubicacionName.Equals(ubicacion.UbicacionName))
                return BadRequest("La ubicacion no existe.");

            var deleted = await _service.DeleteAsync(ubicacionName);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
