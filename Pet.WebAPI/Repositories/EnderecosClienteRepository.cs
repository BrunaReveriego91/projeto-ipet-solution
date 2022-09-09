using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Pet.WebAPI.Repositories
{
    public class EnderecosClienteRepository : BaseRepository<EnderecoCliente, PetContext>, IEnderecosClienteRepository
    {
        public EnderecosClienteRepository(PetContext context) : base(context)
        {
        }

        public override IEnumerable<EnderecoCliente> GetAll(Expression<Func<EnderecoCliente, bool>>? expression = null)
        {
            return base.GetAll(expression);
        }

        public override EnderecoCliente? Get(int id)
        {
            return base.Get(id);
        }

        public override Task Update(EnderecoCliente entity)
        {
            return base.Update(entity);
        }

        public override Task Delete(EnderecoCliente entity)
        {
            return base.Delete(entity);
        }
    }
}
