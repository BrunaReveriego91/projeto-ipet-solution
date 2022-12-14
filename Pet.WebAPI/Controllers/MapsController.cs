using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Interfaces.Controllers;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class MapsController : Controller, IMapsController
    {
        private IMapsService _service;

        public MapsController(IMapsService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPrestadoresByUserLocation(string userId)
        {
            return Ok(await _service.GetPrestadoresByUserLocation(userId));
        }

    }
}
