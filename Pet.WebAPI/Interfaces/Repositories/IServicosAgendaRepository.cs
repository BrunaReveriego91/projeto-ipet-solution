using Pet.WebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IServicosAgendaRepository
    {
        Task<ServicoAgenda> Add(int id_agenda, int id_servico_prestador, int id_endereco_prestador, string mensagem_cliente);
        ServicoAgenda? Get(int id);
        void Delete(ServicoAgenda servico);
    }
}
