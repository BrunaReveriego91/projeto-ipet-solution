using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IPetsRepository
    {
        Task<Pets> Add(Pets pets);
        Pets? Get(int id);
        Task Update(Pets pets);
        Task Delete(Pets pets);
        IEnumerable<Pets> GetAll();
    }
}
