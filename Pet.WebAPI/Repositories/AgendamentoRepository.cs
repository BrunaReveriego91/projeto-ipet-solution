using Microsoft.EntityFrameworkCore;
using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class AgendamentoRepository : BaseRepository<Agenda, PetContext>, IAgendamentoRepository
    {
        public AgendamentoRepository(PetContext context) : base(context)
        {
        }

        public override Agenda? Get(int id)
        {
            var query = (from p in DataContext.Agendamentos
                         where p.Id == id
                         select p)
                         .Include(s => s.Servicos)
                         .FirstOrDefault();
            return query;
        }
    }
}
