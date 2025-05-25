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

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TipoMaquinaDto>>> GetAllTiposMaquina()
        {
            var tipos = await _service.GetAllAsync();
            return Ok(tipos);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<TipoMaquinaDto>> GetTipoMaquinaByName(string tipoMaquina)
        {
            var tipo = await _service.GetByNameAsync(tipoMaquina);
            if (tipo == null)
                return NotFound();

            return Ok(tipo);
        }

        [HttpPost]
        public async Task<ActionResult<TipoMaquinaDto>> PostTipoMaquina(TipoMaquinaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTipoMaquinaByName), new { Tipo = created }, created);
        }

        //[HttpPut]
        //public async Task<IActionResult> PutTipoMaquina(string tipoMaquina, TipoMaquinaDto dto)
        //{
        //    var tipo = await _service.GetByNameAsync(tipoMaquina);
        //    if (tipo == null || !tipoMaquina.Equals(tipo.Tipo))
        //        return BadRequest("El tipo de maquinaria no existe.");

        //    var updated = await _service.UpdateAsync(dto);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteTipoMaquina(string tipoMaquina)
        {
            var tipo = await _service.GetByNameAsync(tipoMaquina);
            if (tipo == null || !tipoMaquina.Equals(tipo.Tipo))
                return BadRequest("El tipo de maquinaria no existe.");

            var deleted = await _service.DeleteAsync(tipoMaquina);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
