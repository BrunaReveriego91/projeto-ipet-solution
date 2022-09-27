using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class MapsService : IMapsService
    {
        private IMapsRepository _mapsRepository;

        public MapsService(IMapsRepository mapsRepository)
        {
            _mapsRepository = mapsRepository;
        }

        public IEnumerable<Prestador> GetPrestadoresByUserLocation(int userId)
        {
            var service = _mapsRepository.GetPrestadoresByUserLocation(userId);
            if (service is null)
                throw new Exception($"Não foram localizados prestadores próximos ou cliente não foi localizado.");
            
            return service;
        }
    }
}
