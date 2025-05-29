using Bussines_Logic_Layer.DTOs.Reembolso;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReembolsoController : Controller
    {
        private readonly IReembolsoService _service;

        public ReembolsoController(IReembolsoService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ReembolsoDto>>> GetReembolsos()
        {
            var reembolsos = await _service.GetAllAsync();
            return Ok(reembolsos);
        }
        [HttpGet("byDNI")]
        public async Task<ActionResult<IEnumerable<ReembolsoDto?>>> GetReembolsosByDNI(string dni)
        {
            var reembolsos = await _service.GetByDNIAsync(dni);
            return Ok(reembolsos);
        }

        [HttpGet("byId")]
        public async Task<ActionResult<ReembolsoDto>> GetReembolso(int id)
        {
            var reembolso = await _service.GetByIdAsync(id);
            if (reembolso == null)
                return NotFound();

            return Ok(reembolso);
        }

        [HttpPost]
        public async Task<ActionResult<ReembolsoDto>> PostReembolso(CreateReembolsoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetReembolso), new { id = created }, created);
        }

        [HttpPut("byId")]
        public async Task<IActionResult> PutReembolso(int id, ReembolsoDto dto)
        {
            var reembolso = await _service.GetByIdAsync(id);
            if (reembolso == null || !id.Equals(reembolso.idReembolso))
                return BadRequest("El reembolso no existe.");

            var updated = await _service.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("byId")]
        public async Task<IActionResult> DeleteReembolso(int id)
        {
            var reembolso = await _service.GetByIdAsync(id);
            if (reembolso == null || !id.Equals(reembolso.idReembolso))
                return BadRequest("El reembolso no existe.");

            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }


    }
}
