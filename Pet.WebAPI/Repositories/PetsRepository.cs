using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class PetsRepository : BaseRepository<Pets, PetContext>, IPetsRepository
    {
        public PetsRepository(PetContext context) : base(context)
        {
        }


        public override Pets? Get(int id)
        {
            var query = (from p in DataContext.Pets
                         where p.Id == id
                         select p).FirstOrDefault();

            return query;
        }

        public override Task Update(Pets pets)
        {
            return base.Update(pets);
        }

        public override async Task Delete(Pets pets)
        {
            await base.Delete(pets);
        }
        public override IEnumerable<Pets> GetAll()
        {
            return base.GetAll();
        }
    }
}
