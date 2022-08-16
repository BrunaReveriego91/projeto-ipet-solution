using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class PrestadoresService : IPrestadoresService
    {
        private readonly IPrestadoresRepository _repository;

        public PrestadoresService(IPrestadoresRepository repository)
        {
            _repository = repository;
        }

        public async Task<Prestador> Add(NovoPrestador novoPrestador)
        {
            var prestador = new Prestador()
            {
                NomeCompleto = novoPrestador.NomeCompleto,
                CPF_CNPJ = novoPrestador.CPF_CNPJ
            };
            
            return await _repository.Add(prestador);
        }

        public async Task Delete(int id)
        {
            var entry = _repository.Get(id);

            if (entry is null)
            {
                throw new Exception($"Prestador não encontrado pelo Id {id}.");
            }

            await _repository.Delete(entry);
        }

        public Prestador? Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Prestador> GetPrestadores()
        {
            return _repository.GetAll();
        }

        public async Task Update(int id, AlterarPrestador entity)
        {
            var entry = _repository.Get(id);

            if (entry is null)
            {
                throw new Exception($"Prestador não encontrado pelo Id {id}.");
            }

            entry.NomeCompleto = entity.NomeCompleto;
            entry.CPF_CNPJ = entity.CPF_CNPJ;

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
