using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Pet.WebAPI.Repositories
{
    public class EnderecosPrestadorRepository : BaseRepository<EnderecoPrestador, PetContext>, IEnderecosPrestadorRepository
    {
        public EnderecosPrestadorRepository(PetContext context) : base(context)
        {
        }

        //public override IEnumerable<EnderecoPrestador> GetAll(Expression<Func<EnderecoPrestador, bool>>? expression = null)
        //{
        //    return base.GetAll(expression);
        //}

        //public override EnderecoPrestador? Get(int id)
        //{
        //    return base.Get(id);
        //}

        //public override Task Update(EnderecoPrestador entity)
        //{
        //    return base.Update(entity);
        //}

        //public override Task Delete(EnderecoPrestador entity)
        //{
        //    return base.Delete(entity);
        //}
    }
}
