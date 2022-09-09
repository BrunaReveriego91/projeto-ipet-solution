using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IAgendamentoController
    {
        Task<IActionResult> PostAgendamento(NovoAgendamento novoAgendamento);
        Task<IActionResult> PutAgendamento(int id, AlterarAgendamento alterarAgendamento);
        IActionResult DeleteAgendamento(int id);
        //ActionResult<List<ServicoAgenda>> GetServicosAgenda(int id);
        ActionResult<Agenda?> GetAgendamento(int id);
    }
}
