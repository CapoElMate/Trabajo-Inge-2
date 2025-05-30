using Bussines_Logic_Layer.DTOs.Publicacion;
using Bussines_Logic_Layer.Interfaces;
using Domain_Layer.Entidades;
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

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PublicacionDto>>> GetPublicaciones()
        {
            var publicaciones = await _service.GetAllAsync();
            return Ok(publicaciones);
        }

        [HttpGet("byId")]
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

        [HttpPut("byId")]
        public async Task<IActionResult> PutPublicacion(int id, PublicacionDto dto)
        {
            var publi = await _service.GetByIdAsync(id);
            if (publi == null || !id.Equals(publi.idPublicacion))
                return BadRequest("La publicacion no existe.");

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("byId")]
        public async Task<IActionResult> DeletePublicacion(int id)
        {
            var publi = await _service.GetByIdAsync(id);
            if (publi == null || !id.Equals(publi.idPublicacion))
                return BadRequest("La publicacion no existe.");

            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("byId/logic")]
        public async Task<IActionResult> LogicDeletePublicacion(int id)
        {
            var publi = await _service.GetByIdAsync(id);
            if (publi == null || !id.Equals(publi.idPublicacion))
                return BadRequest("La publicacion no existe.");

            var deleted = await _service.LogicDeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

    }
}
