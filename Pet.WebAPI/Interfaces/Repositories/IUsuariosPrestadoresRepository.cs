using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IUsuariosPrestadoresRepository
    {
        Task<UsuarioPrestador> Add(UsuarioPrestador entity);
    }
}
