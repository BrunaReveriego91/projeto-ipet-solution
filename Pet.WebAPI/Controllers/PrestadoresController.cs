using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Services;
using Pet.WebAPI.Services;

namespace Pet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class PrestadoresController : Controller, IPrestadoresController
    {
        private readonly IPrestadoresService _service;

        public PrestadoresController(IPrestadoresService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> NovoPrestador([FromBody] NovoPrestador prestador)
        {
            await _service.Add(prestador);
            return Ok();
        }

    }

    public interface IPrestadoresController
    {
        IActionResult Get(int id);
        Task<IActionResult> NovoPrestador([FromBody] NovoPrestador prestador);
    }

}
