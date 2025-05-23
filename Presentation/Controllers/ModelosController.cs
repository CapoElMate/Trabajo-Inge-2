using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly IModeloService _service;

        public ModelosController(IModeloService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModeloDto>>> GetModelos()
        {
            var modelos = await _service.GetAllAsync();
            return Ok(modelos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModeloDto>> GetModelo(string modeloName)
        {
            var modelo = await _service.GetByNameAsync(modeloName);
            if (modelo == null)
                return NotFound();

            return Ok(modelo);
        }

        [HttpPost]
        public async Task<ActionResult<ModeloDto>> PostMaquina(ModeloDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetModelo), new { ModeloName = created }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelo(string modeloName, ModeloDto dto)
        {
            if(!modeloName.Equals(dto.Modelo))
                return BadRequest();

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(string modeloName)
        {
            var deleted = await _service.DeleteAsync(modeloName);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
