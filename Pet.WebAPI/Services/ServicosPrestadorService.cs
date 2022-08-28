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
        public ServicosPrestadorService(IServicosPrestadorRepository repository)
        {
            _repository = repository;
        }
        public async Task<ServicoPrestador> Add(NovoServicoPrestador servico)
        {
            var result = await _repository.Add(new ServicoPrestador(servico.PrestadorId, servico.ServicoId, servico.Ativo));
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
