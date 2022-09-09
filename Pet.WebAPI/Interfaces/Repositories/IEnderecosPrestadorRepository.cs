using Pet.WebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IEnderecosPrestadorRepository
    {
        Task<EnderecoPrestador> Add(EnderecoPrestador entity);
        IEnumerable<EnderecoPrestador> GetAll(Expression<Func<EnderecoPrestador, bool>>? expression = null);
        EnderecoPrestador? Get(int id);
        Task Update(EnderecoPrestador entity);
        Task Delete(EnderecoPrestador entity);
    }
}
