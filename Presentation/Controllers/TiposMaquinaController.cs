using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TiposMaquinaController : ControllerBase
    {
        private readonly ITipoMaquinaService _service;

        public TiposMaquinaController(ITipoMaquinaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMaquinaDto>>> GetTiposMaquina()
        {
            var tipos = await _service.GetAllAsync();
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMaquinaDto>> GetTipoMaquina(string tipoMaquina)
        {
            var tipo = await _service.GetByNameAsync(tipoMaquina);
            if (tipo == null)
                return NotFound();

            return Ok(tipo);
        }

        [HttpPost]
        public async Task<ActionResult<TipoMaquinaDto>> PostMaquina(TipoMaquinaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTipoMaquina), new { Tipo = created }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoMaquina(string tipoMaquina, TipoMaquinaDto dto)
        {
            if (!tipoMaquina.Equals(dto.Tipo))
                return BadRequest();

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(string tipoMaquina)
        {
            var deleted = await _service.DeleteAsync(tipoMaquina);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
