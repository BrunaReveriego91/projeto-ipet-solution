using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IServicosController
    {
        Task<IActionResult> PostServico(NovoServico servico);
        Task<IActionResult> PutServico(int id, AlterarServico servico);
        IActionResult DeleteServico(int id);
        ActionResult<List<Servico>> GetServicos();
        ActionResult<Servico?> GetServico(int id);
    }
}
