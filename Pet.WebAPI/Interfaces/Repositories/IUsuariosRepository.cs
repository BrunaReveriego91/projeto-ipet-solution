using Pet.WebAPI.Domain;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IUsuariosRepository
    {
        Task<Usuario> Add(Usuario entity);
        Task<UsuarioPrestador> AddUsuarioPrestador(NovoUsuarioPrestador usuario);
        //Task<UsuarioCliente> AddUsuarioCliente(NovoUsuarioCliente usuario);
    }
}
