using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Controllers
{
    [Route("petclient")]
    [ControllerName("ClientPet")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class ClientPetController : Controller
    {
        private readonly IClientPetService _clientPetService;

        public ClientPetController(IClientPetService clientPetService)
        {
            _clientPetService = clientPetService;
        }

        [HttpPost]
        public async Task<IActionResult> AddClientPetAsync([FromBody] ClientPet clientPet)
        {
            await _clientPetService.Add(clientPet);
            return Ok();
        }

        [HttpGet]
        public async Task<List<ClientPet>> GetClientPetAsync()
        {
            return await _clientPetService.ListClientPets();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClientPetAsync([FromBody] ClientPet clientPet)
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
