using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IClientesService
    {
        Task<Cliente> Add(Cliente clientPet);
        Task Update(int id,Cliente clientPet);
        Task Delete(int id);
        Cliente? Get(int id);
        IEnumerable<Cliente> GetClientes();
    }
}
