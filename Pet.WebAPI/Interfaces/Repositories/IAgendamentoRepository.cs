using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using System.Linq.Expressions;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IAgendamentoRepository
    {
        Task<Agenda> Add(Agenda agendamento);
        Agenda? Get(int id);
        Task Update(Agenda agenda);
        //Task Delete(Agenda agenda);
        void Delete(Agenda agenda);
    }
}
