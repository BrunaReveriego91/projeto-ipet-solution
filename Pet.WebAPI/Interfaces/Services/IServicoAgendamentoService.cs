using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IServicosAgendaService
    {
        /// <summary>
        /// Cancelar Servico do Agendamento
        /// </summary>
        /// <param name="id_agenda"></param>
        /// <param name="id_servico"></param>
        Task Delete(int id_agenda, int id_servico);

        /// <summary>
        /// Concluir Servico do Agendamento (por parte do prestador)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="servico"></param>
        /// <returns></returns>
        Task Complete(int id_agenda, int id_servico, ConcluirServicoAgenda servico);
        
        /// <summary>
        /// Alterar Servico do Agendamento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="servico"></param>
        /// <returns></returns>
        Task Update(int id, AlterarServicoAgenda servico);
    }
}
