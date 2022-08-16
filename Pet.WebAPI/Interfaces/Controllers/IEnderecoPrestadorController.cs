using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IEnderecoPrestadorController
    {
        Task<IActionResult> PostEnderecoPrestador(NovoEnderecoPrestador endereco);
        ActionResult<List<EnderecoPrestador>> GetEnderecosPrestador(int prestador_id);
        Task<IActionResult> PutEnderecoPrestador(int id, AlterarEnderecoPrestador endereco);
        IActionResult DeleteEnderecoPrestador(int id);
    }
}
