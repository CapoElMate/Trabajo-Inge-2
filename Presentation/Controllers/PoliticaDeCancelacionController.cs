using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PoliticaDeCancelacionController : ControllerBase
    {
        private readonly IPoliticaDeCancelacionService _service;

        public PoliticaDeCancelacionController(IPoliticaDeCancelacionService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PoliticaDeCancelacionDto>>> GetPoliticaDeCancelacion()
        {
            var PoliticaDeCancelacion = await _service.GetAllAsync();
            return Ok(PoliticaDeCancelacion);
        }

        /*
        [HttpGet("byId")]
        public async Task<ActionResult<PoliticaDeCancelacionDto>> GetPoliticaDeCancelacion(string politica)
        {
            var PoliticaDeCancelacion = await _service.GetByIdAsync(politica);
            if (PoliticaDeCancelacion == null)
                return NotFound();

            return Ok(PoliticaDeCancelacion);
        }*/

        [HttpPost]
        public async Task<ActionResult<PoliticaDeCancelacionDto>> PostPoliticaDeCancelacion(PoliticaDeCancelacionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            if(created == null)
            {
                return Conflict("La politica de cancelacion ya existe.");
            }
            else
            {
                return CreatedAtAction(nameof(GetPoliticaDeCancelacion),  created);
            }
        }

        /*
        [HttpPut]
        public async Task<IActionResult> PutPoliticaDeCancelacion(string politica, PoliticaDeCancelacionDto dto)
        {
            var PoliticaDeCancelacion = await _service.GetByIdAsync(politica);
            if (PoliticaDeCancelacion == null || politica.Equals(PoliticaDeCancelacion.Politica))
                return BadRequest("La politica de cancelacion no existe.");

            var updated = await _service.UpdateAsync(politica, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }*/

        [HttpDelete("byId")]
        public async Task<IActionResult> DeletePoliticaDeCancelacion(string politica)
        {
            var PoliticaDeCancelacion = await _service.GetByNameAsync(politica);
            if (PoliticaDeCancelacion == null || !politica.Equals(PoliticaDeCancelacion.Politica))
                return BadRequest("La politica de cancelacion no existe.");

            var deleted = await _service.DeleteAsync(politica);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        //[HttpDelete("byId/logic")]
        //public async Task<IActionResult> LogicDeletePoliticaDeCancelacion(string politica)
        //{
        //    var PoliticaDeCancelacion = await _service.GetNameAsync(politica);
        //    if (PoliticaDeCancelacion == null || politica.Equals(PoliticaDeCancelacion.Politica))
        //        return BadRequest("La politica de cancelacion no existe.");

        //    var deleted = await _service.LogicDeleteAsync(politica);
        //    if (!deleted)
        //        return NotFound();

        //    return NoContent();
        //}
    }
}
