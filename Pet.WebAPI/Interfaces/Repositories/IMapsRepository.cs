using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IMapsRepository
    {
        IEnumerable<Prestador> GetPrestadoresByUserLocation(string userId);
    }
}
