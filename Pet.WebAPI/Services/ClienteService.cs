using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class ClienteService : IClienteService
    {
        private IClienteRepository _clientPetRepository;

        public ClienteService(IClienteRepository clientPetRepository)
        {
            _clientPetRepository = clientPetRepository;
        }

        public async Task Add(Cliente clientPet)
        {
            await _clientPetRepository.Add(clientPet);
        }

        public async Task Delete(int id)
        { 
            await _clientPetRepository.Delete(id);
        }

        public async Task<List<Cliente>> ListClientPets()
        {
            return await Task.Run(() => _clientPetRepository.ListClientPets());
        }

        public async Task Update(Cliente clientPet)
        {
            await _clientPetRepository.Update(clientPet);
        }
    }
}
