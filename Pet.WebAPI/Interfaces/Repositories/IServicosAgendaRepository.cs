using Pet.WebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IServicosAgendaRepository
    {
        /// <summary>
        /// Adicionar
        /// </summary>
        /// <param name="id_agenda"></param>
        /// <param name="id_servico_prestador"></param>
        /// <param name="id_endereco_prestador"></param>
        /// <param name="mensagem_cliente"></param>
        /// <returns></returns>
        Task<ServicoAgenda> Add(int id_agenda, int id_servico_prestador, int id_endereco_prestador, string mensagem_cliente);
        
        /// <summary>
        /// Obter Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServicoAgenda? Get(int id);

        /// <summary>
        /// Excluir
        /// </summary>
        /// <param name="servico"></param>
        void Delete(ServicoAgenda servico);

        Task Update(ServicoAgenda entity);

        Task Complete(ServicoAgenda entity);
    }
}
