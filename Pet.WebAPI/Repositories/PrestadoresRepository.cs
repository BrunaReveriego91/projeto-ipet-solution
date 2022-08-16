using Microsoft.EntityFrameworkCore;
using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Repositories
{
    public class PrestadoresRepository : BaseRepository<Prestador, PetContext>, IPrestadoresRepository
    {
        public PrestadoresRepository(PetContext context) : base(context)
        {
        }

        public override Prestador? Get(int id)
        {
            var query = (from p in DataContext.Prestadores
                         where p.Id == id
                         select p)
                         .Include(e => e.Enderecos).FirstOrDefault();
            return query;
        }

        public override Task Update(Prestador prestador)
        {
            return base.Update(prestador);
        }

        public override async Task Delete(Prestador prestador)
        {
            await base.Delete(prestador);
        }

        public override IEnumerable<Prestador> GetAll()
        {
            return base.GetAll();
        }
    }

    public interface IPrestadoresRepository
    {
        Task<Prestador> Add(Prestador prestador);
        Prestador? Get(int id);
        Task Update(Prestador prestador);
        Task Delete(Prestador prestador);
        IEnumerable<Prestador> GetAll();
    }
}
