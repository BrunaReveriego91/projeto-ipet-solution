using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Controllers;
using Pet.WebAPI.Interfaces.Repositories;
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
        private readonly IServicosAgendaService _serviceAgendaService;

        public AgendamentoController(IAgendamentoService service, IServicosAgendaService serviceAgendaService)
        {
            _service = service;
            _serviceAgendaService = serviceAgendaService;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAgendamento(int id)
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

        #region "SERVIÇOS AGENDA"

        /// <summary>
        /// Alterar Serviço do Agendamento
        /// </summary>
        /// <param name="servicoAgenda"></param>
        /// <param name="id_agenda"></param>
        /// <param name="id_servico"></param>
        /// <returns></returns>
        [HttpPut("{id_agenda}/Servico/{id_servico}")]
        public async Task<IActionResult> PutServicoAgendamento([FromBody] AlterarServicoAgenda servicoAgenda, [FromRoute] int id_agenda, [FromRoute] int id_servico)
        {
            try
            {
                await _serviceAgendaService.Update(id_servico, servicoAgenda);
            }
            catch (Exception)
            {
                return NoContent();
            }

            return Ok();
        }

        [HttpPost("{id_agenda}/Servico/{id_servico}")]
        public async Task<IActionResult> PostConcluirServicoAgendamento([FromBody] ConcluirServicoAgenda servicoAgenda, [FromRoute] int id_agenda, [FromRoute] int id_servico)
        {
            try
            {
                await _serviceAgendaService.Complete(id_agenda, id_servico, servicoAgenda);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id_agenda}/Servico/{id_servico}")]
        public async Task<IActionResult> CancelarServicoAgendamento([FromRoute] int id_agenda, [FromRoute] int id_servico)
        {
            try
            {
                await _serviceAgendaService.Delete(id_agenda, id_servico);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Ok();
        }

        #endregion


    }

}
