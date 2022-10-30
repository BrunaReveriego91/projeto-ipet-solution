using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IServicosPrestadorController
    {
        Task<IActionResult> PostServicoPrestador(List<NovoServicoPrestador> servico);
        ActionResult<List<ServicoPrestador>> GetServicosPrestador(int prestador_id);
        Task<IActionResult> PutServicoPrestador(int id, AlterarServicoPrestador servico);
        IActionResult DeleteServicoPrestador(int id);
    }
}
