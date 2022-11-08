using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IServicosPrestadorService
    {
        Task<List<ServicoPrestador>> Add(List<NovoServicoPrestador> servico);
        List<ServicoPrestador>? GetAllFromPrestador(int prestador_id);
        Task Update(int id, AlterarServicoPrestador servico);
        //Task Delete(int id);
        void Delete(int id);
    }
}
