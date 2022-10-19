using Microsoft.EntityFrameworkCore;
using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

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
                         .Include(e => e.Enderecos)
                         .Include(s => s.Servicos)
                         .FirstOrDefault();
            return query;
        }
    }
}
