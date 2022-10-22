using Microsoft.EntityFrameworkCore;
using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class ServicosPrestadorRepository : BaseRepository<ServicoPrestador, PetContext>, IServicosPrestadorRepository
    {
        public ServicosPrestadorRepository(PetContext context) : base(context)
        {
        }

        public override void Delete(ServicoPrestador entity)
        {
            try
            {
                base.Delete(entity);
            }
            catch (DbUpdateException)
            {
                throw new Exception($"Não é possível excluir o ServicoPrestador, o mesmo já deve ter Agendamentos cadastrados.");
            }
        }
    }
}
