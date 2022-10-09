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
    public class PetsController : Controller, IPetsController
    {
        private readonly IPetsService _petsService;

        public PetsController(IPetsService petsService)
        {
            _petsService = petsService;
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePet(int id)
        {
            try
            {
                _petsService.Delete(id);
            }
            catch (Exception)
            {
                return NoContent();
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllPets()
        {
            return Ok(_petsService.GetPets());
        }

        [HttpGet("{id}")]
        public IActionResult GetPet(int id)
        {
            return Ok(_petsService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostPet([FromBody] NovoPet novoPet)
        {
            var result = await _petsService.Add(novoPet);
            return CreatedAtAction(nameof(GetPet), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, AlterarPet pet)
        {
            try
            {
                await _petsService.Update(id, pet);
            }
            catch (Exception)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
