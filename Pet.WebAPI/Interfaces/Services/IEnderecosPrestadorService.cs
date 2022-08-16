using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IEnderecosPrestadorService
    {
        Task<EnderecoPrestador> Add(NovoEnderecoPrestador novo);

        /// <summary>
        /// Obtém todos os endereços do Prestador.
        /// </summary>
        /// <param name="prestador_id"></param>
        /// <returns></returns>
        List<EnderecoPrestador>? GetAll(int prestador_id);

        Task Update(int id, AlterarEnderecoPrestador endereco);
        Task Delete(int id);
    }
}
