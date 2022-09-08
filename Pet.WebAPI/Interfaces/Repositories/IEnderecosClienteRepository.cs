using Pet.WebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IEnderecosClienteRepository
    {
        Task<EnderecoCliente> Add(EnderecoCliente entity);
        IEnumerable<EnderecoCliente> GetAll(Expression<Func<EnderecoCliente, bool>>? expression = null);
        EnderecoCliente? Get(int id);
        Task Update(EnderecoCliente entity);
        Task Delete(EnderecoCliente entity);
    }
}
