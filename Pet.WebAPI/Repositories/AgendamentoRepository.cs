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
    }
}
