using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class ServicosPrestadorRepository : BaseRepository<ServicoPrestador, PetContext>, IServicosPrestadorRepository
    {
        public ServicosPrestadorRepository(PetContext context) : base(context)
        {
        }

        //public async Task Activate(int id, bool activate = true)
        //{
        //    var entry = base.Get(id);

        //    if (entry != null)
        //    {
        //        entry.Ativo = activate;
        //        await base.Update(entry);
        //    }
        //}
    }
}
