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

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TagMaquinaDto>>> GetAllTagsMaquina()
        {
            var tags = await _service.GetAllAsync();
            return Ok(tags);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<TagMaquinaDto>> GetTagMaquinaByName(string tagMaquina)
        {
            var tag = await _service.GetByNameAsync(tagMaquina);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult<TagMaquinaDto>> PostTagMaquina(TagMaquinaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTagMaquinaByName), new { Tag = created }, created);
        }

        //[HttpPut]
        //public async Task<IActionResult> PutMaquina(string tagMaquina, TagMaquinaDto dto)
        //{
        //    var tag = await _service.GetByNameAsync(tagMaquina);
        //    if (tag == null || !tagMaquina.Equals(tag.Tag))
        //        return BadRequest("El tag no existe.");

        //    var updated = await _service.UpdateAsync(dto);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteTagMaquina(string tagMaquina)
        {
            var tag = await _service.GetByNameAsync(tagMaquina);
            if (tag == null || !tagMaquina.Equals(tag.Tag))
                return BadRequest("El tag no existe.");

            var deleted = await _service.DeleteAsync(tagMaquina);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
