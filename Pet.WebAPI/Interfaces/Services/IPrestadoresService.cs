using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IPrestadoresService
    {
        Task<Prestador> Add(NovoPrestador novoPrestador);
        Prestador? Get(int id);
        Task Update(int id, AlterarPrestador entity);
        //Task Delete(int id);
        void Delete(int id);
        IEnumerable<Prestador> GetPrestadores(); 
    }
}
