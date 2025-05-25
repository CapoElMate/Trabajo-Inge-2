using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : Controller
    {
        private readonly IPublicacionService _service;

        public PublicacionController(IPublicacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicacionDto>>> GetPublicaciones()
        {
            var publicaciones = await _service.GetAllAsync();
            return Ok(publicaciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublicacionDto>> GetPublicacion(int id)
        {
            var publicacion = await _service.GetByIdAsync(id);
            if (publicacion == null)
                return NotFound();

            return Ok(publicacion);
        }

        [HttpPost]
        public async Task<ActionResult<PublicacionDto>> PostPublicacion(CreatePublicacionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetPublicacion), new { id = created }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicacion(int id, PublicacionDto dto)
        {
            if (id != dto.idPublicacion)
                return BadRequest();

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicacion(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
        

    }
}
