using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IClientPetRepository
    {
        Task Add(ClientPet clientPet);
        Task Update(ClientPet clientPet);
        Task Delete(int id);
        Task<List<ClientPet>> ListClientPets();
    }
}
