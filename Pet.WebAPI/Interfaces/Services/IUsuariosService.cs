using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IUsuariosService
    {
        /// <summary>
        /// Adiciona um Novo Usuário como Prestador.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<UsuarioPrestador> AddUsuarioPrestador(NovoUsuarioPrestador usuario);

        //Task<UsuarioCliente> AddUsuarioCliente(NovoUsuarioCliente cliente);
    }
}
