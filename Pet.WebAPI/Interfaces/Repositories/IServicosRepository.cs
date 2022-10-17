using Pet.WebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IServicosRepository
    {
        Task<Servico> Add(Servico servico);
        IEnumerable<Servico> GetAll(Expression<Func<Servico, bool>>? expression = null);
        Servico? Get(int id);
        Task Update(Servico servico);
        //Task Delete(Servico servico);
        void Delete(Servico servico);
    }
}
