using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Entities.Maps;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IMapsService
    {
        Task<IEnumerable<PrestadorMaps>> GetPrestadoresByUserLocation(int userId);
      
    }
}
