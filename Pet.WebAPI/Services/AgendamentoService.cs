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

        public AgendamentoService(IAgendamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Agenda> Add(NovoAgendamento novoAgendamento)
        {
            var agenda = new Agenda()
            {
                ClienteId = novoAgendamento.Id_Cliente,
                PrestadorId = novoAgendamento.Id_Prestador,
                Data_Agenda = novoAgendamento.Data_Agendamento
            };
            var result = await _repository.Add(agenda);
            return result;
        }

        public async Task Delete(int id)
        {
            var entry = Get(id);

            if (entry is null)
            {
                throw new Exception($"Agendamento não encontrado pelo Id {id}.");
            }

            await _repository.Delete(entry);
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

            if (entry is null)
            {
                throw new Exception($"Agendamento não encontrado pelo Id {id}.");
            }

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
