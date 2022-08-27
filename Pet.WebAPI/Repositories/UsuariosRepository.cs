using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class UsuariosRepository : BaseRepository<Usuario, PetContext>, IUsuariosRepository
    {
        private readonly IPrestadoresRepository _prestadoresRepository;
        private readonly IUsuariosPrestadoresRepository _usuariosPrestadoresRepository;

        public UsuariosRepository(PetContext context, IPrestadoresRepository prestadoresRepository, 
            IUsuariosPrestadoresRepository usuariosPrestadoresRepository) : base(context)
        {
            _prestadoresRepository = prestadoresRepository;
            _usuariosPrestadoresRepository = usuariosPrestadoresRepository;
        }

        public async Task<UsuarioPrestador> AddUsuarioPrestador(NovoUsuarioPrestador novo_usuario)
        {
            // Adiciona o Novo Usuário.
            var usuario = new Usuario()
            {
                Nome = novo_usuario.Nome_Usuario,
                Password = novo_usuario.Senha,
                EMail = novo_usuario.EMail
            };

            var usu_adicionado = await base.Add(usuario);

            // Adiciona os dados do Prestador.
            var prest = new Prestador()
            {
                NomeCompleto = novo_usuario.Dados_Prestador.Nome_Completo,
                CPF_CNPJ = novo_usuario.Dados_Prestador.CPF_CNPJ,
                Telefone = novo_usuario.Dados_Prestador.Telefone,
                WhatsApp = novo_usuario.Dados_Prestador.WhatsApp,
                //Enderecos = new List<EnderecoPrestador>()
                //{
                //    {
                //        new EnderecoPrestador()
                //        {
                //             Logradouro = usuario.Dados_Prestador.EnderecosPrestador[]
                //        }
                //    }
                //}
            };

            await _prestadoresRepository.Add(prest);

            // Faz o vínculo.
            var result = await _usuariosPrestadoresRepository.Add(new UsuarioPrestador(usu_adicionado.Id, prest.Id));

            return result;
        }

        public override Task<Usuario> Add(Usuario entity)
        {
            return base.Add(entity);
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
}
