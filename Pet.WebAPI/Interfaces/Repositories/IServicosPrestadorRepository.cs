using Pet.WebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IServicosPrestadorRepository
    {
        Task<ServicoPrestador> Add(ServicoPrestador servico);
        ServicoPrestador? Get(int id);
        IEnumerable<ServicoPrestador> GetAll(Expression<Func<ServicoPrestador, bool>>? expression = null);
        //Task Delete(ServicoPrestador servico);
        void Delete(ServicoPrestador servico);
        Task Update(ServicoPrestador servico);
        //Task Activate(int id, bool activate = true);
    }
}
