using Bussines_Logic_Layer.DTOs.Maquina;
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

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<MaquinaDto>>> GetMaquinas()
        {
            var maquinas = await _service.GetAllAsync();
            return Ok(maquinas);
        }

        [HttpGet("byId")]
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

        [HttpPut]
        public async Task<IActionResult> PutMaquina(int id, MaquinaDto dto)
        {
            var maquina = await _service.GetByIdAsync(id);
            if (maquina == null || id != maquina.IdMaquina)
                return BadRequest("La maquina no existe.");

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMaquina(int id)
        {
            var maquina = await _service.GetByIdAsync(id);
            if (maquina == null || id != maquina.IdMaquina)
                return BadRequest("La maquina no existe.");

            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
