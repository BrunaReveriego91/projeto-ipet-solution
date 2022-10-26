using Pet.WebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IEnderecosClienteRepository
    {
        /// <summary>
        /// Adicionar Endereço Cliente
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<EnderecoCliente> Add(EnderecoCliente entity);

        //IEnumerable<EnderecoCliente> GetAll(Expression<Func<EnderecoCliente, bool>>? expression = null);

        /// <summary>
        /// Obter Endereço do Cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EnderecoCliente? Get(int id);

        /// <summary>
        /// Atualizar Endereço do Cliente
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(EnderecoCliente entity);

        //void Delete(EnderecoCliente entity);
    }
}
