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
        private IServicosPrestadorService _servicosPrestadorService;
        private readonly EarthAPIConnection _apiConnection;

        public MapsService(IMapsRepository mapsRepository, IOptions<EarthAPIConnection> apiConnection, IServicosPrestadorService servicosPrestadorService)
        {
            _mapsRepository = mapsRepository;
            _apiConnection = apiConnection.Value;
            _servicosPrestadorService = servicosPrestadorService;
        }

        public async Task<IEnumerable<PrestadorMaps>> GetPrestadoresByUserLocation(int userId)
        {
            var prestadores = _mapsRepository.GetPrestadoresByUserLocation(userId).Where(x => x.Enderecos != null);
            if (prestadores is null)
                throw new Exception($"Não foram localizados prestadores próximos ou cliente não foi localizado.");

            var prestadoresMaps = new List<PrestadorMaps>();

            foreach (var prestador in prestadores)
            {
                var listaServicoPrestador = _servicosPrestadorService.GetAllFromPrestador(prestador.Id);
                foreach (var enderecoPrestador in prestador.Enderecos)
                {
                    var localizacao = await ProcuraGeolocalizacaoPrestador(enderecoPrestador);
                    if(localizacao != null)
                    {
                        var prestadorMap = new PrestadorMaps();
                        prestadorMap.NomeCompleto = prestador.NomeCompleto;
                        prestadorMap.Servicos = listaServicoPrestador;
                        prestadorMap.Latitude = localizacao[0];
                        prestadorMap.Longitude = localizacao[1];

                        prestadoresMaps.Add(prestadorMap);
                    }
                }
            }

            return prestadoresMaps;
        }

        private async Task<double[]> ProcuraGeolocalizacaoPrestador(EnderecoPrestador? enderecoPrestador)
        {

            var key = _apiConnection.Key;

            var postal_code = enderecoPrestador.CEP;
            var address = string.Concat(enderecoPrestador.Logradouro, ' ', enderecoPrestador.Numero);
            var coordenadas = new double[2];
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
                            coordenadas[0] = item.resources.FirstOrDefault().point.coordinates[0];
                            coordenadas[1] = item.resources.FirstOrDefault().point.coordinates[1];
                        }
                    }
                }
            }
            return coordenadas;
        }
    }
}
