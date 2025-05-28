using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Alquiler;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquilerController : Controller
    {
        private readonly IAlquilerService _service;

        public AlquilerController(IAlquilerService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<AlquilerDto>>> GetAlquileres()
        {
            var alquileres = await _service.GetAllAsync();
            return Ok(alquileres);
        }

        [HttpGet("byId")]
        public async Task<ActionResult<AlquilerDto>> GetAlquiler(int id)
        {
            var alquiler = await _service.GetByIdAsync(id);
            if (alquiler == null)
                return NotFound();

            return Ok(alquiler);
        }

        [HttpPost]
        public async Task<ActionResult<AlquilerDto>> PostAlquiler(CreateAlquilerDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAlquiler), new { id = created }, created);
        }

        [HttpPut("byId")]
        public async Task<IActionResult> PutAlquiler(int id, AlquilerDto dto)
        {
            var publi = await _service.GetByIdAsync(id);
            if (publi == null || !id.Equals(publi.idAlquiler))
                return BadRequest("El alquiler no existe.");

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("byId")]
        public async Task<IActionResult> DeleteAlquiler(int id)
        {
            var alquiler = await _service.GetByIdAsync(id);
            if (alquiler == null || !id.Equals(alquiler.idAlquiler))
                return BadRequest("El alquiler no existe.");

            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("byId/logic")]
        public async Task<IActionResult> LogicDeleteAlquiler(int id)
        {
            var alquiler = await _service.GetByIdAsync(id);
            if (alquiler == null || !id.Equals(alquiler.idAlquiler))
                return BadRequest("El alquiler no existe.");

            var deleted = await _service.LogicDeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }


    }
}
