using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IPrestadoresRepository
    {
        Task<Prestador> Add(Prestador prestador);
        Prestador? Get(int id);
        IEnumerable<Agenda> GetAgendamentosPrestador(int prestador_id);
        Task Update(Prestador prestador);
        //Task Delete(Prestador prestador);
        void Delete(Prestador prestador);
        IEnumerable<Prestador> GetAll();
    }
}
