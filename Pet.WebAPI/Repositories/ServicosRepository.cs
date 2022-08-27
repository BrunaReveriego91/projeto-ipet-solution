using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class ServicosRepository : BaseRepository<Servico, PetContext>, IServicosRepository
    {
        public ServicosRepository(PetContext context) : base(context)
        {
        }
    }
}
