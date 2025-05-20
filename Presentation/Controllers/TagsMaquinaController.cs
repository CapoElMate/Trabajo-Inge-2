using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TagsMaquina : ControllerBase
    {
        private readonly ITagMaquinaService _service;

        public TagsMaquina(ITagMaquinaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagMaquinaDto>>> GetTagsMaquina()
        {
            var tags = await _service.GetAllAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagMaquinaDto>> GetTagMaquina(string tagMaquina)
        {
            var tag = await _service.GetByNameAsync(tagMaquina);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult<TagMaquinaDto>> PostMaquina(TagMaquinaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTagMaquina), new { Tag = created }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquina(string tagMaquina, TagMaquinaDto dto)
        {
            if (!tagMaquina.Equals(dto.Tag))
                return BadRequest();

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(string tagMaquina)
        {
            var deleted = await _service.DeleteAsync(tagMaquina);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
