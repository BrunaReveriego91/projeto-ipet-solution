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
        ActionResult<Agenda?> GetAgendamento(int id);

        #region "Serviço Agenda"
        Task<IActionResult> PutServicoAgendamento(AlterarServicoAgenda servicoAgenda, int id_agenda, int id_servico);
        Task<IActionResult> PostConcluirServicoAgendamento(ConcluirServicoAgenda servicoAgenda, int id_agenda, int id_servico);
        Task<IActionResult> CancelarServicoAgendamento(int id_agenda, int id_servico);
        #endregion
    }
}
