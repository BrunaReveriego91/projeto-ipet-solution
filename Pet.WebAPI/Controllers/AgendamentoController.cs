using Microsoft.AspNetCore.Http;
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
    public class AgendamentoController : Controller, IAgendamentoController
    {
        private readonly IAgendamentoService _service;

        public AgendamentoController(IAgendamentoService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAgendamento(int id)
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

        [HttpGet("{id}")]
        public ActionResult<Agenda?> GetAgendamento(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAgendamento([FromBody] NovoAgendamento novoAgendamento)
        {
            try
            {
                var result = await _service.Add(novoAgendamento);
                return CreatedAtAction(nameof(GetAgendamento), new { id = result.Id }, result);
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
        public async Task<IActionResult> PutAgendamento(int id, AlterarAgendamento alterarAgendamento)
        {
            try
            {
                await _service.Update(id, alterarAgendamento);
            }
            catch (Exception)
            {
                return NoContent();
            }

            return Ok();
        }
    }

}
