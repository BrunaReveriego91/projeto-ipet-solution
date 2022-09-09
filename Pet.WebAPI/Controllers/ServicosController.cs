using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain;
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
    public class ServicosController : Controller, IServicosController
    {
        private readonly IServicosServices _service;

        public ServicosController(IServicosServices service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteServico(int id)
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
        public ActionResult<Servico?> GetServico(int id)
        {
            var dados = _service.Get(id);
            if (dados is null) return NoContent();
            return Ok(dados);
        }

        [HttpGet()]
        public ActionResult<List<Servico>> GetServicos()
        {
            var dados = _service.GetAll();
            if (dados is null) return NoContent();
            return Ok(dados);
        }

        [HttpPost]
        public async Task<IActionResult> PostServico([FromBody] NovoServico servico)
        {
            var result = await _service.Add(servico);
            return CreatedAtAction(nameof(GetServico), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutServico(int id, AlterarServico servico)
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
