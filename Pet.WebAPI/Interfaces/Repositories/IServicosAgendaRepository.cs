using Pet.WebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IServicosAgendaRepository
    {
        //Task<ServicoAgenda> Add(ServicoAgenda servico);
        Task<ServicoAgenda> Add(int id_agenda, int id_servico, int id_endereco_prestador, string mensagem_cliente);
        ServicoAgenda? Get(int id);
        Task Delete(ServicoAgenda servico);
    }
}
