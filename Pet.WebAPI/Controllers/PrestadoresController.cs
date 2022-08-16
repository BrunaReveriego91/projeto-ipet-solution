using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Controllers;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class PrestadoresController : Controller, IPrestadoresController
    {
        private readonly IPrestadoresService _service;

        public PrestadoresController(IPrestadoresService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePrestador(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (Exception)
            {
                return NoContent();
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllPrestadores()
        {
            return Ok(_service.GetPrestadores());
        }

        [HttpGet("{id}")]
        public IActionResult GetPrestador(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostPrestador([FromBody] NovoPrestador prestador)
        {
            var result = await _service.Add(prestador);
            return CreatedAtAction(nameof(GetPrestador), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestador(int id, AlterarPrestador prestador)
        {
            try
            {
                await _service.Update(id, prestador);
            }
            catch (Exception)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
