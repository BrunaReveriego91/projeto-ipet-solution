using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IEnderecosClienteService
    {
        Task<EnderecoCliente> Add(NovoEnderecoCliente novoEndereco);

        /// <summary>
        /// Obtém todos os endereços do Cliente.
        /// </summary>
        /// <param name="cliente_id"></param>
        /// <returns></returns>
        List<EnderecoCliente>? GetAll(int cliente_id);

        Task Update(int id, AlterarEnderecoCliente endereco);
        Task Delete(int id);
    }
}
