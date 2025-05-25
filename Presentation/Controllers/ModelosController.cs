using System.Net;
using Bussines_Logic_Layer.DTOs;
using Bussines_Logic_Layer.Interfaces;
using Domain_Layer.Entidades;
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

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ModeloDto>>> GetModelos()
        {
            var modelos = await _service.GetAllAsync();
            return Ok(modelos);
        }

        [HttpGet("byName")]
        public async Task<ActionResult<ModeloDto>> GetModelo(string modeloName)
        {
            var modelo = await _service.GetByNameAsync(modeloName);
            if (modelo == null)
                return NotFound();

            return Ok(modelo);
        }

        [HttpPost]
        public async Task<ActionResult<ModeloDto>> PostModelo(ModeloDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetModelo), new { ModeloName = created }, created);
        }

        //[HttpPut]
        //public async Task<IActionResult> PutModelo(string modeloName, ModeloDto dto)
        //{
        //    var modelo = await _service.GetByNameAsync(modeloName);
        //    if(modelo == null || !modelo.Modelo.Equals(modeloName))
        //        return BadRequest("El modelo no existe.");

        //    var updated = await _service.UpdateAsync(modeloName, dto);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteModelo(string modeloName)
        {
            var modelo = await _service.GetByNameAsync(modeloName);
            if (modelo == null || !modelo.Modelo.Equals(modeloName))
                return BadRequest("El modelo no existe.");

            var deleted = await _service.DeleteAsync(modeloName);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
