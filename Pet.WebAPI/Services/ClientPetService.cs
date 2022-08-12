using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class ClientPetService : IClientPetService
    {
        private IClientPetRepository _clientPetRepository;

        public ClientPetService(IClientPetRepository clientPetRepository)
        {
            _clientPetRepository = clientPetRepository;
        }

        public async Task Add(ClientPet clientPet)
        {
            await _clientPetRepository.Add(clientPet);
        }

        public async Task Delete(int id)
        { 
            await _clientPetRepository.Delete(id);
        }

        public async Task<List<ClientPet>> ListClientPets()
        {
            return await Task.Run(() => _clientPetRepository.ListClientPets());
        }

        public async Task Update(ClientPet clientPet)
        {
            await _clientPetRepository.Update(clientPet);
        }
    }
}
