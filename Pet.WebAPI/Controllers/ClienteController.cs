using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Controllers
{
    [Route("cliente")]
    [ControllerName("Cliente")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clientPetService;

        public ClienteController(IClienteService clientPetService)
        {
            _clientPetService = clientPetService;
        }

        [HttpPost]
        public async Task<IActionResult> AddClientPetAsync([FromBody] Cliente clientPet)
        {
            await _clientPetService.Add(clientPet);
            return Ok();
        }

        [HttpGet]
        public async Task<List<Cliente>> GetClientPetAsync()
        {
            return await _clientPetService.ListClientPets();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClientPetAsync([FromBody] Cliente clientPet)
        {
            await _clientPetService.Update(clientPet);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteClientPetAsync([FromQuery] QueryParameters queryParameters)
        {

            await _clientPetService.Delete(queryParameters.Id);
            return Ok();
        }
    }
}
