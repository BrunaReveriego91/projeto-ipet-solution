using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IEnderecosClienteService
    {
        Task<EnderecoCliente> Add(NovoEnderecoCliente novoEndereco);

        //List<EnderecoCliente>? GetAll(int cliente_id);

        EnderecoCliente Get(int id);

        Task Update(int id, AlterarEnderecoCliente endereco);

        //Task Delete(int id);
        //void Delete(int id);
    }
}
