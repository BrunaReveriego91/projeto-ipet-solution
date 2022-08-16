using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task Add(Cliente clientPet);
        Task Update(Cliente clientPet);
        Task Delete(int id);
        Task<List<Cliente>> ListClientPets();
    }
}
