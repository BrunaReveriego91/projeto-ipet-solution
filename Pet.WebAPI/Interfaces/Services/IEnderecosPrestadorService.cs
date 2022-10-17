using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IEnderecosPrestadorService
    {
        Task<EnderecoPrestador> Add(NovoEnderecoPrestador novo);

        List<EnderecoPrestador>? GetAll(int prestador_id);

        Task Update(int id, AlterarEnderecoPrestador endereco);
        //Task Delete(int id);
        void Delete(int id);
    }
}
