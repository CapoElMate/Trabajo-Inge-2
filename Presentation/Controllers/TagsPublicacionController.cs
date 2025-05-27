using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TagsPublicacion : ControllerBase
    {
        private readonly ITagPublicacionService _service;

        public TagsPublicacion(ITagPublicacionService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TagMaquinaDto>>> GetAllTagsPublicacion()
        {
            var tags = await _service.GetAllAsync();
            return Ok(tags);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<TagMaquinaDto>> GetTagPublicacionByName(string tagPublicacion)
        {
            var tag = await _service.GetByNameAsync(tagPublicacion);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult<TagPublicacionDto>> PostTagPublicacion(TagPublicacionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTagPublicacionByName), new { Tag = created }, created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTagPublicacion(string tagPublicacion)
        {
            var tag = await _service.GetByNameAsync(tagPublicacion);
            if (tag == null || !tagPublicacion.Equals(tag.Tag))
                return BadRequest("El tag no existe.");

            var deleted = await _service.DeleteAsync(tagPublicacion);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
