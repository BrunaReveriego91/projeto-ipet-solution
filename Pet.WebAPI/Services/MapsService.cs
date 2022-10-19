using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Entities.Maps;
using Pet.WebAPI.Domain.Settings;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;
using System.Configuration;
using System.Dynamic;

namespace Pet.WebAPI.Services
{
    public class MapsService : IMapsService
    {
        private IMapsRepository _mapsRepository;
        private readonly EarthAPIConnection _apiConnection;

        public MapsService(IMapsRepository mapsRepository, IOptions<EarthAPIConnection> apiConnection)
        {
            _mapsRepository = mapsRepository;
            _apiConnection = apiConnection.Value;
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
                    var localizacao = ProcuraGeolocalizacaoPrestador(enderecoPrestador);
                }
            }

            return null;
        }

        private async Task<List<double>> ProcuraGeolocalizacaoPrestador(EnderecoPrestador? enderecoPrestador)
        {

            var key = _apiConnection.Key;

            var postal_code = enderecoPrestador.CEP;
            var address = string.Concat(enderecoPrestador.Logradouro, ' ', enderecoPrestador.Numero);
            var coordenadas = new List<double>();
            var url = "http://dev.virtualearth.net/REST/v1/Locations?postalCode=" + postal_code + "&key=" + key + "&addressLine=" + address;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {

                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        Maps maps = JsonConvert.DeserializeObject<Maps>(responseBody);

                        foreach (var item in maps.resourceSets.ToList())
                        {
                            coordenadas.Add(item.resources.FirstOrDefault().point.coordinates[0]);
                            coordenadas.Add(item.resources.FirstOrDefault().point.coordinates[1]);
                        }
                    }
                }
            }
            return coordenadas;
        }
    }
}
