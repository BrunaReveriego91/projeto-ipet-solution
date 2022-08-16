using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Repositories
{
    public class ServicosPrestadorRepository : BaseRepository<ServicoPrestador, PetContext>, IServicosPrestadorRepository
    {
        public ServicosPrestadorRepository(PetContext context) : base(context)
        {
        }
    }

    public interface IServicosPrestadorRepository
    {
    }
}
