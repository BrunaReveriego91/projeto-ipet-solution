using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IEnderecoClienteController
    {
        Task<IActionResult> PostEnderecoCliente(NovoEnderecoCliente endereco);
        ActionResult<List<EnderecoCliente>> GetEnderecosCliente(int cliente_id);
        Task<IActionResult> PutEnderecoCliente(int id, AlterarEnderecoCliente endereco);
        IActionResult DeleteEnderecoCliente(int id);
    }
}
