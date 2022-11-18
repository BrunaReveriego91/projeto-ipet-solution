using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Entities.Maps;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IMapsService
    {
        Task<List<PrestadorMaps>> GetPrestadoresByUserLocation(string userId);

    }
}
