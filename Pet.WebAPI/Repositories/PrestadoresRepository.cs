using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace Pet.WebAPI.Repositories
{
    public class PrestadoresRepository : BaseRepository<Prestador, PetContext>, IPrestadoresRepository
    {
        public PrestadoresRepository(PetContext context) : base(context)
        {
        }

        public override Prestador? Get(int id)
        {
            return base.Get(id);
        }
    }

    public interface IPrestadoresRepository
    {
        Task Add(Prestador prestador);
        Prestador? Get(int id);
    }
}
