using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IClienteController
    {
        IActionResult GetCliente(int id);
        Task<IActionResult> PostCliente([FromBody] Cliente cliente);
        Task<IActionResult> PutCliente(int id, Cliente cliente);
        IActionResult DeleteCliente(int id);
        IActionResult GetAllClientes();
    }
}
