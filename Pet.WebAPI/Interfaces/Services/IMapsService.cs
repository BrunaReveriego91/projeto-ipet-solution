using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Entities.Maps;

namespace Pet.WebAPI.Interfaces.Services
{
    public interface IMapsService
    {
        IEnumerable<Maps> GetPrestadoresByUserLocation(int userId);
        Task ProcuraGeolocalizacaoPrestador(EnderecoPrestador? enderecoPrestador);
    }
}
