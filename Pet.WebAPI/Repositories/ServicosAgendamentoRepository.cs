using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class ServicosAgendaRepository : BaseRepository<ServicoAgenda, PetContext>, IServicosAgendaRepository
    {
        private readonly IEnderecosPrestadorRepository _enderecosPrestadorRepository;
        private readonly IServicosRepository _servicosRepository;

        public ServicosAgendaRepository(PetContext context,
            IEnderecosPrestadorRepository enderecosPrestadorRepository,
            IServicosRepository servicosRepository) : base(context)
        {
            _enderecosPrestadorRepository = enderecosPrestadorRepository;
            _servicosRepository = servicosRepository;
        }

        public Task<ServicoAgenda> Add(int id_agenda, int id_servico, int id_endereco_prestador, string mensagem_cliente)
        {
            var servico = _servicosRepository.Get(id_servico);
            if (servico is null)
            {
                throw new NullReferenceException($"Servico não encontrado pelo Id {id_servico}.");
            }

            var enderecoPrestador = _enderecosPrestadorRepository.Get(id_endereco_prestador);
            if (enderecoPrestador is null)
            {
                throw new NullReferenceException($"Endereco_Prestador não encontrado pelo Id {id_endereco_prestador}.");
            }

            var servico_agendamento = new ServicoAgenda()
            {
                AgendaId = id_agenda,
                ServicoId = id_servico,
                EnderecoPrestadorId = id_endereco_prestador,
                Mensagem_Cliente = mensagem_cliente,
                EnderecoPrestador = enderecoPrestador,
                Servico = servico
            };

            return base.Add(servico_agendamento);
        }
    }
}
