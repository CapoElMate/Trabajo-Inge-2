using Bussines_Logic_Layer.DTOs.InfoAsentada;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoAsentadaController : Controller
    {
        private readonly IInfoAsentadaService _service;

        public InfoAsentadaController(IInfoAsentadaService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<InfoAsentadaDto>>> GetInfoAsentadaes()
        {
            var infoAsentadas = await _service.GetAllAsync();
            return Ok(infoAsentadas);
        }

        [HttpGet("byId")]
        public async Task<ActionResult<InfoAsentadaDto>> GetInfoAsentada(int id)
        {
            var infoAsentada = await _service.GetByIdAsync(id);
            if (infoAsentada == null)
                return NotFound();

            return Ok(infoAsentada);
        }

        [HttpPost]
        public async Task<ActionResult<InfoAsentadaDto>> PostInfoAsentada(CreateInfoAsentadaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetInfoAsentada), new { id = created }, created);
        }

        [HttpPut("byId")]
        public async Task<IActionResult> PutInfoAsentada(int id, InfoAsentadaDto dto)
        {
            var infoAsentada = await _service.GetByIdAsync(id);
            if (infoAsentada == null || !id.Equals(infoAsentada.idInfo))
                return BadRequest("La info asentada no existe.");

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("byId")]
        public async Task<IActionResult> DeleteInfoAsentada(int id)
        {
            var infoAsentada = await _service.GetByIdAsync(id);
            if (infoAsentada == null || !id.Equals(infoAsentada.idInfo))
                return BadRequest("La info asentada no existe.");

            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
