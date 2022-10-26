using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Pet.WebAPI.Repositories
{
    public class ServicosAgendaRepository : BaseRepository<ServicoAgenda, PetContext>, IServicosAgendaRepository
    {
        private readonly IEnderecosPrestadorRepository _enderecosPrestadorRepository;
        private readonly IServicosPrestadorRepository _servicosPrestadorRepository;

        public ServicosAgendaRepository(PetContext context,
            IEnderecosPrestadorRepository enderecosPrestadorRepository,
            IServicosPrestadorRepository servicosPrestadorRepository) : base(context)
        {
            _enderecosPrestadorRepository = enderecosPrestadorRepository;
            _servicosPrestadorRepository = servicosPrestadorRepository;
        }

        public Task<ServicoAgenda> Add(int agenda_id, int servico_prestador_id, int endereco_prestador_id, string mensagem_cliente)
        {
            var servico = _servicosPrestadorRepository.Get(servico_prestador_id);
            if (servico is null)
            {
                throw new NullReferenceException($"Servico do Prestador não encontrado pelo Id {servico_prestador_id}.");
            }

            var enderecoPrestador = _enderecosPrestadorRepository.Get(endereco_prestador_id);
            if (enderecoPrestador is null)
            {
                throw new NullReferenceException($"Endereco_Prestador não encontrado pelo Id {endereco_prestador_id}.");
            }

            var servico_agendamento = new ServicoAgenda()
            {
                AgendaId = agenda_id,
                ServicoPrestadorId = servico_prestador_id,
                EnderecoPrestadorId = endereco_prestador_id,
                Mensagem_Cliente = mensagem_cliente,
                EnderecoPrestador = enderecoPrestador,
                ServicoPrestador = servico
            };

            return base.Add(servico_agendamento);
        }

        public Task Complete(ServicoAgenda entity)
        {
            return base.Update(entity);
        }
    }
}
