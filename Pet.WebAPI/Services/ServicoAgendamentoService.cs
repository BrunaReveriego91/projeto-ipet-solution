using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class ServicosAgendaService : IServicosAgendaService
    {
        private readonly IServicosAgendaRepository _servicosAgendaRepository;
        private readonly IAgendamentoRepository _agendamentoRepository;

        public ServicosAgendaService(IServicosAgendaRepository servicosAgendaRepository, IAgendamentoRepository agendamentoRepository)
        {
            _servicosAgendaRepository = servicosAgendaRepository;
            _agendamentoRepository = agendamentoRepository;
        }

        public async Task Complete(int id_agenda, int id_servico, ConcluirServicoAgenda servico)
        {
            try
            {
                var agenda = _agendamentoRepository.Get(id_agenda);

                if (agenda is null)
                {
                    throw new Exception($"Agenda Id {id_agenda} não encontrada.");
                }

                var entry = agenda.Servicos.Where(x => x.Id == id_servico).FirstOrDefault();

                if (entry is null)
                {
                    throw new Exception($"Serviço Id {id_servico} não encontrado.");
                }

                if (entry.Data_Conclusao.HasValue)
                {
                    throw new Exception($"Serviço já foi concluído em {entry.Data_Conclusao.GetValueOrDefault().ToShortDateString()}.");
                }

                entry.Mensagem_Profissional_Executante = servico.Mensagem_Profissional_Executante;
                entry.Data_Conclusao = DateTime.Now;
                entry.Valor_Desconto = servico.Valor_Desconto;

                await _servicosAgendaRepository.Complete(entry);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task Delete(int id_agenda, int id_servico)
        {
            try
            {
                var agenda = _agendamentoRepository.Get(id_agenda);

                if (agenda is null)
                {
                    throw new Exception($"Agenda Id {id_agenda} não encontrada.");
                }

                var entry = agenda.Servicos.Where(x => x.Id == id_servico).FirstOrDefault();

                if (entry is null)
                {
                    throw new Exception($"Serviço Id {id_servico} não encontrado.");
                }

                entry.Data_Cancelamento = DateTime.Now;

                await _servicosAgendaRepository.Update(entry);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task Update(int id, AlterarServicoAgenda servico)
        {
            var entry = _servicosAgendaRepository.Get(id);

            entry.Mensagem_Cliente = servico.Mensagem_Cliente;

            try
            {
                await _servicosAgendaRepository.Update(entry);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}
