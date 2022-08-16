using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Repositories
{
    public class UsuariosRepository : BaseRepository<Usuario, PetContext>, IUsuariosRepository
    {
        private readonly IPrestadoresRepository _prestadoresRepository;

        public UsuariosRepository(PetContext context, IPrestadoresRepository prestadoresRepository) : base(context)
        {
            _prestadoresRepository = prestadoresRepository;
        }

        public Task<Usuario> Add(NovoUsuario usuario, TiposUsuario tipo)
        {
            throw new NotImplementedException();
        }

        //public async Task<Usuario> Add(NovoUsuario usuario, TiposUsuario tipo)
        //{
        //    if (tipo == TiposUsuario.Prestador)
        //    {
        //        //Adiciona o usuário.
        //        var usu = new Usuario()
        //        {
        //            Nome = usuario.Nome,
        //            EMail = usuario.EMail,
        //            Password = usuario.Senha
        //        };

        //        var resultUsu = await base.Add(usu);

        //        var prest = new Prestador()
        //        {

        //        };

        //        _prestadoresRepository.Add()

        //        // TODO: Adiciona o prestador com dados iniciais.
        //    }
        //}
    }

    internal interface IUsuariosRepository
    {
        Task<Usuario> Add(NovoUsuario usuario, TiposUsuario tipo);
    }
}
