using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IServicosServices
    {
        Task<Servico> Add(NovoServico servico);
        Servico? Get(int id);
        List<Servico>? GetAll();
        Task Update(int id, AlterarServico servico);
        //Task Delete(int id);
        void Delete(int id);
    }
}