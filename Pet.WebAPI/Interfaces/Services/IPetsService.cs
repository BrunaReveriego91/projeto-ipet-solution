using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IPetsService
    {
        Task<Pets> Add(NovoPet novoPet);
        Pets? Get(int id);
        Task Update(int id, AlterarPet entity);
        //Task Delete(int id);
        void Delete(int id);
        IEnumerable<Pets> GetPets();
    }
}
