using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IAgendamentoService
    {
        Task<Agenda> Add(NovoAgendamento novoAgendamento);
        Agenda? Get(int id);
        Task Update(int id, AlterarAgendamento entity);
        Task Delete(int id);
        IEnumerable<ServicoAgenda> GetServicos(int id);
    }
}
