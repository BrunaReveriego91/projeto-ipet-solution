using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class ServicosPrestadorService : IServicosPrestadorService
    {
        private readonly IServicosPrestadorRepository _repository;
        private readonly IPrestadoresRepository _prestadoresRepository;
        private readonly IServicosRepository _servicosRepository;

        public ServicosPrestadorService(IServicosPrestadorRepository repository, IPrestadoresRepository prestadoresRepository, IServicosRepository servicosRepository)
        {
            _repository = repository;
            _prestadoresRepository = prestadoresRepository;
            _servicosRepository = servicosRepository;
        }
        public async Task<ServicoPrestador> Add(NovoServicoPrestador novoServico)
        {
            // Verifica se o Prestador existe
            var prestador = _prestadoresRepository.Get(novoServico.Prestador_Id);

            if (prestador is null)
            {
                throw new NullReferenceException($"Prestador não encontrado pelo Id {novoServico.Prestador_Id}.");
            }

            // Verifica se o Serviço existe
            var servico = _servicosRepository.Get(novoServico.Servico_Id);

            if (servico is null)
            {
                throw new NullReferenceException($"Serviço não encontrado pelo Id {novoServico.Servico_Id}.");
            }

            var srv_prest = new ServicoPrestador()
            {
                Prestador = prestador,
                Servico = servico,
                PrestadorId = novoServico.Prestador_Id,
                ServicoId = novoServico.Servico_Id,
                Ativo = novoServico.Ativo,
                Valor = novoServico.Valor,
            };

            var result = await _repository.Add(srv_prest);
            return result;
        }

        public async Task Delete(int id)
        {
            var entry = _repository.Get(id);
            if (entry is null) { return; }
            await _repository.Delete(entry);
        }

        public List<ServicoPrestador>? GetAllFromPrestador(int prestador_id)
        {
            var servicos = _repository.GetAll(p => p.PrestadorId == prestador_id);
            return servicos.ToList();
        }

        public async Task Update(int id, AlterarServicoPrestador servico)
        {
            var entry = _repository.Get(id);

            if (entry is null)
            {
                throw new Exception($"Serviço não encontrado pelo Id {id}.");
            }

            entry.Ativo = servico.Ativo;
            entry.Valor = servico.Valor;

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
