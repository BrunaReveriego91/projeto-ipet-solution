using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _repository;
        private readonly IClientesRepository _clientesRepository;
        private readonly IPrestadoresRepository _prestadoresRepository;
        private readonly IEnderecosPrestadorRepository _enderecosPrestadorRepository;
        private readonly IServicosPrestadorRepository _servicosPrestadorRepository;
        private readonly IServicosAgendaRepository _servicosAgendaRepository;

        public AgendamentoService(
            IAgendamentoRepository repository,
            IClientesRepository clientesRepository,
            IPrestadoresRepository prestadoresRepository,
            IEnderecosPrestadorRepository enderecosPrestadorRepository,
            IServicosPrestadorRepository servicosPrestadorRepository,
            IServicosAgendaRepository servicosAgendaRepository)
        {
            _repository = repository;
            _clientesRepository = clientesRepository;
            _prestadoresRepository = prestadoresRepository;
            _enderecosPrestadorRepository = enderecosPrestadorRepository;
            _servicosPrestadorRepository = servicosPrestadorRepository;
            _servicosAgendaRepository = servicosAgendaRepository;
        }

        public async Task<Agenda> Add(NovoAgendamento novoAgendamento)
        {
            // Valida o cliente.
            var cliente = _clientesRepository.Get(novoAgendamento.Id_Cliente);

            if (cliente is null)
            {
                throw new NullReferenceException($"Cliente não encontrado pelo Id {novoAgendamento.Id_Cliente}.");
            }

            // Valida o prestador.
            var prestador = _prestadoresRepository.Get(novoAgendamento.Id_Prestador);
            if (prestador is null)
            {
                throw new NullReferenceException($"Prestador não encontrado pelo Id {novoAgendamento.Id_Prestador}.");
            }

            novoAgendamento.Servicos.ForEach(srv =>
            {
                // Valida o serviço do prestador.
                var servico = _servicosPrestadorRepository.Get(srv.Id_Servico_Prestador);
                if (servico is null)
                {
                    throw new NullReferenceException($"Servico do Prestador não encontrado pelo Id {srv.Id_Servico_Prestador}.");
                }

                // Valida o endereço do prestador.
                var enderecoPrestador = _enderecosPrestadorRepository.Get(srv.Id_Endereco_Prestador);
                if (enderecoPrestador is null)
                {
                    throw new NullReferenceException($"Endereco_Prestador não encontrado pelo Id {srv.Id_Endereco_Prestador}.");
                }
            });

            var agenda = new Agenda()
            {
                ClienteId = novoAgendamento.Id_Cliente,
                PrestadorId = novoAgendamento.Id_Prestador,
                Data_Agenda = novoAgendamento.Data_Agendamento,
                Data_Cancelamento = DateTime.MinValue,
                Cliente = cliente,
                Prestador = prestador
            };

            var result_agenda = await _repository.Add(agenda);

            if (result_agenda.Id > 0)
            {
                novoAgendamento.Servicos.ForEach(srv =>
                {
                    var srv_agenda = _servicosAgendaRepository.Add(result_agenda.Id, srv.Id_Servico_Prestador, srv.Id_Endereco_Prestador, srv.Mensagem_Cliente);

                    var srv_result = srv_agenda.Result;
                    //result_agenda.Servicos.Add(srv_result);
                });
            }

            return result_agenda;
        }

        public void Delete(int id)
        {
            var entry = Get(id);

            if (entry is null)
            {
                throw new NullReferenceException($"Agendamento não encontrado pelo Id {id}.");
            }

            _repository.Delete(entry);
        }

        public Agenda? Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<ServicoAgenda> GetServicos(int id)
        {
            var agenda = Get(id);

            if (agenda is null)
            {
                return new List<ServicoAgenda>();
            }

            return agenda.Servicos;
        }

        public async Task Update(int id, AlterarAgendamento entity)
        {
            var entry = Get(id);

            //Eberton, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (entry is null)
            //{
            //    throw new Exception($"Agendamento não encontrado pelo Id {id}.");
            //}

            entry.Data_Agenda = entity.Data_Agenda;

            try
            {
                await _repository.Update(entry);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
