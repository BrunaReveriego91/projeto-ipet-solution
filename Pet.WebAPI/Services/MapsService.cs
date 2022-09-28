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

        public IEnumerable<Maps> GetPrestadoresByUserLocation(int userId)
        {
            var prestadores = _mapsRepository.GetPrestadoresByUserLocation(userId);
            if (prestadores is null)
                throw new Exception($"Não foram localizados prestadores próximos ou cliente não foi localizado.");

            var mapsLocations = new List<Maps>();

            foreach (var prestador in prestadores)
            {
                foreach (var enderecoPrestador in prestador.Enderecos)
                {
                    var location = ProcuraGeolocalizacaoPrestador(enderecoPrestador);
                }
            }

            return null;
        }

        private async Task ProcuraGeolocalizacaoPrestador(EnderecoPrestador enderecoPrestador)
        {
            //Maps map = new();

            var key = "Aps_5kSQcUT8FGgG-RMhSjc71DfwtZEee7IYbz__tU1BuLRh8dyDwj2oj72aQUW_";
            var postal_code = enderecoPrestador.CEP;
            var address = string.Concat(enderecoPrestador.Logradouro, ' ', enderecoPrestador.Numero);

            var url = "http://dev.virtualearth.net/REST/v1/Locations?postalCode=" + postal_code + "&key=" + key + "&addressLine=" + address;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //cliente = JsonConvert.DeserializeObject<ClienteViewModel>(responseBody);
                }
            }
            //return View(cliente);
        }
    }
}
