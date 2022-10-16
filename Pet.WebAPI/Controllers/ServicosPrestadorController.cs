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
    public class ServicosPrestadorController : Controller, IServicosPrestadorController
    {
        private readonly IServicosPrestadorService _service;

        public ServicosPrestadorController(IServicosPrestadorService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteServicoPrestador(int id)
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

        [HttpGet("{prestador_id}")]
        public ActionResult<List<ServicoPrestador>> GetServicosPrestador(int prestador_id)
        {
            var dados = _service.GetAllFromPrestador(prestador_id);
            if (dados is null) return NoContent();
            return Ok(dados);
        }

        [HttpPost]
        public async Task<IActionResult> PostServicoPrestador([FromBody] NovoServicoPrestador servico)
        {
            try
            {
                var result = await _service.Add(servico);
                return CreatedAtAction(nameof(GetServicosPrestador), new { prestador_id = result.PrestadorId }, result);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicoPrestador(int id, AlterarServicoPrestador servico)
        {
            try
            {
                await _service.Update(id, servico);
            }
            catch (Exception)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
