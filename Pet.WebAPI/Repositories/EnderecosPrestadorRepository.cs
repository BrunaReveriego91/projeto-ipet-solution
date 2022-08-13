using Pet.Repository.Infrastructure;
using Pet.WebAPI.Migrations;

namespace Pet.WebAPI.Repositories
{
    public class EnderecosPrestadorRepository : BaseRepository<EnderecoPrestador, PetContext>
    {
        public EnderecosPrestadorRepository(PetContext context) : base(context)
        {
        }
    }
}
