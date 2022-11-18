using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pet.WebAPI.Domain;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Entities.Maps;
using Pet.WebAPI.Domain.Settings;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class MapsService : IMapsService
    {
        private IMapsRepository _mapsRepository;
        private IServicosPrestadorService _servicosPrestadorService;
        private readonly EarthAPIConnection _apiConnection;
        private IServicosServices _servicosService;

        public MapsService(IMapsRepository mapsRepository, IOptions<EarthAPIConnection> apiConnection, IServicosPrestadorService servicosPrestadorService, IServicosServices servicosService)
        {
            _mapsRepository = mapsRepository;
            _apiConnection = apiConnection.Value;
            _servicosPrestadorService = servicosPrestadorService;
            _servicosService = servicosService;
        }

        public async Task<List<PrestadorMaps>> GetPrestadoresByUserLocation(string userId)
        {
            var prestadores = _mapsRepository.GetPrestadoresByUserLocation(userId);

            if (prestadores is null)
                throw new Exception($"Não foram localizados prestadores próximos ou cliente não foi localizado.");

            var prestadoresMaps = new List<PrestadorMaps>();

            foreach (var prestador in prestadores)
            {
                var listaServicoPrestador = _servicosPrestadorService.GetAllFromPrestador(prestador.Id).Where(x => x.Ativo == true);
                if (listaServicoPrestador is not null)
                {
                    List<ServicoMaps> listaservicosPrestadorDetalhes = GetServicosDetalhesPrestadores(listaServicoPrestador);

                    foreach (var enderecoPrestador in prestador.Enderecos)
                    {
                        var localizacao = await ProcuraGeolocalizacaoPrestador(enderecoPrestador);
                        if (localizacao is not null)
                            prestadoresMaps.Add(PrestadorMaps.CriaPrestadorMaps(prestador.Id,prestador.NomeCompleto, listaservicosPrestadorDetalhes, localizacao[0], localizacao[1]));

                    }
                }
            }

            return prestadoresMaps;
        }

        private List<ServicoMaps> GetServicosDetalhesPrestadores(IEnumerable<ServicoPrestador> listaServicoPrestador)
        {
            var servicosPrestador = new List<ServicoMaps>();

            foreach (var servicoPrestador in listaServicoPrestador)
            {
                var servico = _servicosService.Get(servicoPrestador.ServicoId);
                if (servico is not null && servico.Ativo == true)
                    servicosPrestador.Add(new ServicoMaps(servico.Id, servico.Nome, servico.Descricao, servicoPrestador.Ativo, servicoPrestador.Valor));
            }

            return servicosPrestador;
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
                            coordenadas[0] = item.resources.FirstOrDefault().bbox[0];
                            coordenadas[1] = item.resources.FirstOrDefault().bbox[1];
                        }
                    }
                }
            }
            return coordenadas;
        }
    }
}
