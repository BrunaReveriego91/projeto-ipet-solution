using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Repositories
{
    public class ServicosRepository : BaseRepository<Servico, PetContext>, IServicosRepository
    {
        public ServicosRepository(PetContext context) : base(context)
        {
        }
    }

    public interface IServicosRepository
    {
    }
}
