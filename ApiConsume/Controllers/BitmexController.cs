using ApiConsume.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using ApiConsume.Method;
using ApiConsume.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiConsume.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitmexController : ControllerBase
    {
        
        
        private readonly ApiResumeContext _context;


        public BitmexController( ApiResumeContext conte) { _context = conte; }
       
        [HttpPost]
        public async Task<IActionResult> ObtenerDatos()
        {
            RecordDb record = new RecordDb(_context);
            await record.GuardarenDb();
            return Ok();
        
        }


        [HttpGet]
        public async Task<List<DatosDto>> Get()
        {
            RecordDb record = new RecordDb(_context);
            return await record.GetDatos();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<DatosDto> Get(int id)
        {
            RecordDb record = new RecordDb(_context);
            return await record.GetDatosByID(id);
        }

       
        // PUT api/<ValuesController>/5
        [HttpPut]
        public ActionResult<Dato> Put([FromBody] DatosDto dt)
        {
            RecordDb record = new RecordDb(_context);
            record.Actualizar(dt);
            return Ok();

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RecordDb record = new RecordDb(_context);
            record.Borrar(id);
            
        }
    }

}

