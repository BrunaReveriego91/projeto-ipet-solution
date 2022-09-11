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
    public class EnderecoClienteController : Controller,IEnderecoClienteController
    {
        private readonly IEnderecosClienteService _service;

        public EnderecoClienteController(IEnderecosClienteService service)
        {
            _service = service;
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEnderecoCliente(int id)
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
        [HttpGet("{cliente_id}")]
        public ActionResult<List<EnderecoCliente>> GetEnderecosCliente(int cliente_id)
        {
            var dados = _service.GetAll(cliente_id);
            if (dados is null) return NoContent();
            return Ok(dados);
        }

        [HttpPost]
        public async Task<IActionResult> PostEnderecoCliente(NovoEnderecoCliente endereco)
        {
            var result = await _service.Add(endereco);
            return CreatedAtAction(nameof(GetEnderecosCliente), new { cliente_id = result.ClienteId }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnderecoCliente(int id, AlterarEnderecoCliente endereco)
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
