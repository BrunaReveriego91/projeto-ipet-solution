using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class ClientesRepository : BaseRepository<Cliente, PetContext>, IClientesRepository
    {
        public ClientesRepository(PetContext context) : base(context)
        {
        }

        public override Cliente? Get(int id)
        {
            var query = (from p in DataContext.Clientes
                         where p.Id == id
                         select p).FirstOrDefault();

            return query;
        }


        public override async Task Delete(Cliente cliente)
        {
            await base.Delete(cliente);
        }


        public override IEnumerable<Cliente> GetAll()
        {
            return base.GetAll();
        }

        public override Task Update(Cliente cliente)
        {
            return base.Update(cliente);
        }
    }
}
