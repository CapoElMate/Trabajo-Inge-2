using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Domain_Layer.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEntregaController : Controller
    {
        private readonly ITipoEntregaService _service;

        public TipoEntregaController(ITipoEntregaService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TipoEntregaDto>>> GetTiposEntrega()
        {
            var tiposEntrega = await _service.GetAllAsync();
            return Ok(tiposEntrega);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<TipoEntregaDto>> GetTipoEntrega(string tipoEntregaName)
        {
            var tipoEntrega = await _service.GetByIdAsync(tipoEntregaName);
            if (tipoEntrega == null)
                return NotFound();

            return Ok(tipoEntrega);
        }

        [HttpPost]
        public async Task<ActionResult<TipoEntregaDto>> PostTipoEntrega(TipoEntregaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTipoEntrega), new { id = created }, created);
        }

        //[HttpPut("byName")]
        //public async Task<IActionResult> PutTipoEntrega(string tipoEntregaName, TipoEntregaDto dto)
        //{
        //    var tipoEntrega = await _service.GetByIdAsync(tipoEntregaName);
        //    if (tipoEntrega == null || !tipoEntregaName.Equals(tipoEntrega.TipoEntregaName))
        //        return BadRequest("La tipoEntrega no existe.");

        //    var updated = await _service.UpdateAsync(dto);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete("byName")]
        public async Task<IActionResult> DeleteTipoEntrega(string tipoEntregaName)
        {
            var tipoEntrega = await _service.GetByIdAsync(tipoEntregaName);
            if (tipoEntrega == null || !tipoEntregaName.Equals(tipoEntrega.Entrega))
                return BadRequest("el tipoEntrega no existe.");

            var deleted = await _service.DeleteAsync(tipoEntregaName);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
