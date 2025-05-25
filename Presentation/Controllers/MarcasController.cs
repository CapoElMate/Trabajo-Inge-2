using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController: ControllerBase
    {
        private readonly IMarcaService _service;

        public MarcasController(IMarcaService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<MarcaDto>>> GetMarcas()
        {
            var marcas = await _service.GetAllAsync();
            return Ok(marcas);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<MarcaDto>> GetMarca(string marcaName)
        {
            var marca = await _service.GetByNameAsync(marcaName);
            if (marca == null)
                return NotFound();

            return Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult<MarcaDto>> PostMarca(MarcaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetMarca), new { id = created }, created);
        }

        //[HttpPut()]
        //public async Task<IActionResult> PutMaquina(string marcaName, MarcaDto dto)
        //{
        //    var marca = await _service.GetByNameAsync(marcaName);
        //    if (marca == null || !marcaName.Equals(marca.Marca))
        //        return BadRequest("La marca no existe.");

        //    var updated = await _service.UpdateAsync(dto);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete()]
        public async Task<IActionResult> DeleteMarca(string marcaName)
        {
            var marca = await _service.GetByNameAsync(marcaName);
            if (marca == null || !marcaName.Equals(marca.Marca))
                return BadRequest("La marca no existe.");

            var deleted = await _service.DeleteAsync(marcaName);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
