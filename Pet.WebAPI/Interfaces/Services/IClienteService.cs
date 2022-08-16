using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IClienteService
    {
        Task Add(Cliente clientPet);
        Task Update(Cliente clientPet);
        Task Delete(int id);
        Task<List<Cliente>> ListClientPets();
    }
}
