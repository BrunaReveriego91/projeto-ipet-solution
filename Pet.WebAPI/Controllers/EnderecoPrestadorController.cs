using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Controllers;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class EnderecoPrestadorController : Controller, IEnderecoPrestadorController
    {
        private readonly IEnderecosPrestadorService _service;

        public EnderecoPrestadorController(IEnderecosPrestadorService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEnderecoPrestador(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpGet("{prestador_id}")]
        public ActionResult<List<EnderecoPrestador>> GetEnderecosPrestador(int prestador_id)
        {
            var dados = _service.GetAll(prestador_id);
            if (dados is null) return NoContent();
            return Ok(dados);
        }

        [HttpPost]
        public async Task<IActionResult> PostEnderecoPrestador([FromBody] NovoEnderecoPrestador endereco)
        {
            var result = await _service.Add(endereco);
            return CreatedAtAction(nameof(GetEnderecosPrestador), new { prestador_id = result.PrestadorId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnderecoPrestador(int id, AlterarEnderecoPrestador endereco)
        {
            try
            {
                await _service.Update(id, endereco);
            }
            catch (Exception)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
