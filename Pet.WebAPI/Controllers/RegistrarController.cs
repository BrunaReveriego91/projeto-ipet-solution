using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarController : Controller, IRegistrarController
    {
        private readonly IUsuariosService _service;

        public RegistrarController(IUsuariosService usuariosService)
        {
            _service = usuariosService;
        }

        [HttpPost("prestador")]
        public async Task<IActionResult> PostNovoUsuarioPrestador([FromBody] NovoUsuarioPrestador usuario)
        {
            await _service.AddUsuarioPrestador(usuario);
            //return Created(nameof(GetPrestador), new { id = result.Id }, result);
            // Não retornar created por fins de segurança.
            return Ok();
        }

    }

    internal interface IRegistrarController
    {
        /// <summary>
        /// Registrar Novo Usuário como Prestador.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<IActionResult> PostNovoUsuarioPrestador(NovoUsuarioPrestador usuario);

        //Task<IActionResult> PostNovoUsuarioCliente(NovoUsuarioCliente usuario);
    }
}
