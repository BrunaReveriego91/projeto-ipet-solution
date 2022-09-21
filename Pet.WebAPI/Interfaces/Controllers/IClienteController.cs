using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IClienteController
    {
        IActionResult GetCliente(int id);
        Task<IActionResult> PostCliente([FromBody] NovoCliente cliente);
        Task<IActionResult> PutCliente(int id, AlterarCliente cliente);
        Task<IActionResult> DeleteCliente(int id);
        IActionResult GetAllClientes();
    }
}
