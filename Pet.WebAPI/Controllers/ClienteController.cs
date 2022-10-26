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
    public class ClienteController : Controller, IClienteController
    {
        private readonly IClientesService _clientPetService;

        public ClienteController(IClientesService clientPetService)
        {
            _clientPetService = clientPetService;
        }

        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] NovoCliente clientPet)
        {
            var result = await _clientPetService.Add(clientPet);
            return CreatedAtAction(nameof(GetCliente), new { id = result.Id }, result);
        }

        [HttpGet]
        public IActionResult GetAllClientes()
        {
            return Ok(_clientPetService.GetClientes());
        }

        [HttpGet("{id}")]
        public IActionResult GetCliente(int id)
        {
            return Ok(_clientPetService.Get(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, AlterarCliente cliente)
        {
            try
            {
                await _clientPetService.Update(id, cliente);
            }
            catch (Exception)
            {
                return NoContent();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            try
            {
                _clientPetService.Delete(id);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Ok();
        }

        [HttpGet("idUsuario")]
        public IActionResult GetClienteByUserId(string idUsuario)
        {
            return Ok(_clientPetService.GetByUserId(idUsuario));
        }
    }
}
