using Bussines_Logic_Layer.DTOs.Maquina;
using Bussines_Logic_Layer.DTOs.Reserva;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _service;

        public ReservaController(IReservaService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ReservaDto>>> GetReservas()
        {
            var Reservas = await _service.GetAllAsync();
            return Ok(Reservas);
        }

        [HttpGet("byId")]
        public async Task<ActionResult<ReservaDto>> GetReserva(int id)
        {
            var reserva = await _service.GetByIdAsync(id);
            if (reserva == null)
                return NotFound();

            return Ok(reserva);
        }
        [HttpGet("byDNI")]
        public async Task<ActionResult<IEnumerable<ReservaDto?>>> GetReservaByDNI(string DNI)
        {
            var reservas = await _service.GetByDNIAsync(DNI);
            if (reservas == null)
                return NotFound("El cliente no tiene reservas a su nombre.");

            return Ok(reservas);
        }

        [HttpPost]
        public async Task<ActionResult<ReservaDto>> PostReserva(CreateReservaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetReserva), new { id = created }, created);
        }

        [HttpPut]
        public async Task<IActionResult> PutReserva(int id, ReservaDto dto)
        {
            var reserva = await _service.GetByIdAsync(id);
            if (reserva == null || id != reserva.idReserva)
                return BadRequest("La reserva no existe.");

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _service.GetByIdAsync(id);
            if (reserva == null || id != reserva.idReserva)
                return BadRequest("La reserva no existe.");

            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
