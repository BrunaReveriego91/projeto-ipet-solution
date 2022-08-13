using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Repositories;

namespace Pet.WebAPI.Services
{
    public class PrestadoresService : IPrestadoresService
    {
        private readonly IPrestadoresRepository _repository;

        public PrestadoresService(IPrestadoresRepository repository)
        {
            _repository = repository;
        }

        public async Task Add(NovoPrestador novoPrestador)
        {
            var prestador = new Prestador()
            {
                NomeCompleto = novoPrestador.NomeCompleto,
                CPF_CNPJ = novoPrestador.CPF_CNPJ
            };
            
            await _repository.Add(prestador);
        }

        public Prestador? Get(int id)
        {
            return _repository.Get(id);
        }
    }

    public interface IPrestadoresService
    {
        Task Add(NovoPrestador novoPrestador);
        Prestador? Get(int id);
    }
}
