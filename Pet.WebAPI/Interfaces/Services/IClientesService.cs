using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IClientesService
    {
        Task<Cliente> Add(NovoCliente clientPet);
        Task Update(int id, AlterarCliente clientPet);
        //Task Delete(int id);
        void Delete(int id);
        Cliente? Get(int id);
        IEnumerable<Cliente> GetClientes();
    }
}
