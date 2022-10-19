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
                    var location = ProcuraGeolocalizacaoPrestador(enderecoPrestador);
                }
            }

            return null;
        }

        public async Task ProcuraGeolocalizacaoPrestador(EnderecoPrestador? enderecoPrestador)
        {
            //Maps map = new();

            var key = _apiConnection.Key;
            //var postal_code = enderecoPrestador.CEP;
            var postal_code = "04222-060";
            var address = string.Concat("Rua Violantino dos Santos", ' ', "48");

            var url = "http://dev.virtualearth.net/REST/v1/Locations?postalCode=" + postal_code + "&key=" + key + "&addressLine=" + address;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                  
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        Maps maps = JsonConvert.DeserializeObject<Maps>(responseBody);
                        var estabelecimentos = new List<Estabelecimento>();
                        foreach(var item in maps.resourceSets.ToList())
                        {
                            var teste = item.resources.FirstOrDefault().point.coordinates[0];
                            coordinates.Add(teste);
                        }
                    }
                   
                    //cliente = JsonConvert.DeserializeObject<ClienteViewModel>(responseBody);
                }
            }
            //return View(cliente);
        }
    }
}
