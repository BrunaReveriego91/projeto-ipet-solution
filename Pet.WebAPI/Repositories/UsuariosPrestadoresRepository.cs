using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class UsuariosPrestadoresRepository : BaseRepository<UsuarioPrestador, PetContext>, IUsuariosPrestadoresRepository
    {
        public UsuariosPrestadoresRepository(PetContext context) : base(context)
        {
        }

        //public override Task<UsuarioPrestador> Add(UsuarioPrestador entity)
        //{
        //    return base.Add(entity);
        //}
    }


}
