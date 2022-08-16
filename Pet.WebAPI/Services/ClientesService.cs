using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class ClientesService : IClientesService
    {
        private IClientesRepository _clientPetRepository;

        public ClientesService(IClientesRepository clientPetRepository)
        {
            _clientPetRepository = clientPetRepository;
        }

        public async Task<Cliente> Add(Cliente clientPet)
        {
            return await _clientPetRepository.Add(clientPet);
        }

        public async Task Delete(int id)
        {
            var entry = _clientPetRepository.Get(id);

            if (entry is null)
            {
                throw new Exception($"Prestador não encontrado pelo Id {id}.");
            }
            await _clientPetRepository.Delete(entry);
        }

        public Cliente? Get(int id)
        {
            return _clientPetRepository.Get(id);
        }


        public IEnumerable<Cliente> GetClientes()
        {
            return _clientPetRepository.GetAll();
        }

        public async Task Update(int id, Cliente clientPet)
        {
            var entry = _clientPetRepository.Get(id);

            if (entry is null)
            {
                throw new Exception($"Prestador não encontrado pelo Id {id}.");
            }

            entry.NomeCompleto = clientPet.NomeCompleto;
            entry.CPF = clientPet.CPF;

            try
            {
                await _clientPetRepository.Update(entry);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
