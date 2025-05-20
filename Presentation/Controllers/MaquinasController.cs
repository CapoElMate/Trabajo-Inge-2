using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MaquinasController : ControllerBase
    {
        private readonly IMaquinaService _service;

        public MaquinasController(IMaquinaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaquinaDto>>> GetMaquinas()
        {
            var maquinas = await _service.GetAllAsync();
            return Ok(maquinas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaquinaDto>> GetMaquina(int id)
        {
            var maquina = await _service.GetByIdAsync(id);
            if (maquina == null)
                return NotFound();

            return Ok(maquina);
        }

        [HttpPost]
        public async Task<ActionResult<MaquinaDto>> PostMaquina(CreateMaquinaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetMaquina), new { id = created }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquina(int id, MaquinaDto dto)
        {
            if (id != dto.IdMaquina)
                return BadRequest();

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
