using Microsoft.AspNetCore.Mvc;
using System;

using Data_Access_Layer;
using Domain_Layer.Entidades;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;


namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRegistradoController : ControllerBase
    {
        //NOTA: le paso el contecto de aplication para que me permita acceder a la base de datos.
        private readonly ApplicationDbContext _context;
        public UsuarioRegistradoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public List<UsuarioRegistrado> Get()
        {

            //hardcodeo los valores que devuelve el Controler    

            //UsuarioRegistrado ur1 =  new UsuarioRegistrado("pepe@gmail.com", "1122234", "a", false, "pepe", "gomez",
            //    20, "221-1111111", "calle", "altura", "dpto", "calle 1 y calle 2", true);

            //UsuarioRegistrado ur2 = new UsuarioRegistrado("maria@gmail.com", "1122233", "b", false, "maria", "perez",
            //    50, "221-1111111", "calle", "altura", "dpto", "calle 1 y calle 2", true);

            List<UsuarioRegistrado> usuarios = new List<UsuarioRegistrado>();

            //usuarios.Add(ur1);
            //usuarios.Add(ur2);

            return usuarios;
        }

        // GET api/<PrubeaController>/5
        [HttpGet("{id}")]
        public UsuarioRegistrado Get(int id)
        {
            //return new UsuarioRegistrado("pepe@gmail.com", id.ToString(), "a", false, "pepe", "mujica",
            //    20, "221-1111111", "calle", "altura", "dpto", "calle 1 y calle 2", true); ;
            return null;
        }

        // POST api/<PrubeaController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<PrubeaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<PrubeaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
