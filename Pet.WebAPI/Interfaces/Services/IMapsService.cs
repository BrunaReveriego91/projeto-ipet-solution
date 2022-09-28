using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IMapsService
    {
        IEnumerable<Maps> GetPrestadoresByUserLocation(int userId);
    }
}
