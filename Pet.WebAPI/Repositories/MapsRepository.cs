using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class MapsRepository : IMapsRepository
    {
        private IClientesRepository _clientesRepository;
        private IPrestadoresRepository _prestadoresRepository;
        private IEnderecosPrestadorRepository _enderecosPrestadorRepository;

        public MapsRepository(IClientesRepository clientesRepository, IPrestadoresRepository prestadoresRepository, IEnderecosPrestadorRepository enderecosPrestadorRepository)
        {
            _clientesRepository = clientesRepository;
            _prestadoresRepository = prestadoresRepository;
            _enderecosPrestadorRepository = enderecosPrestadorRepository;
        }

        public IEnumerable<Prestador> GetPrestadoresByUserLocation(string userId)
        {
            var prestadoresList = new List<Prestador>();

            var cliente = _clientesRepository.GetByUserId(userId);


            if (cliente is null || cliente.Endereco is null)
                return null;

            var enderecosPrestadores = _enderecosPrestadorRepository.GetAll().Where(x => x.Cidade == cliente.Endereco.Cidade && x.UF == cliente.Endereco.UF).ToList();
            if (enderecosPrestadores is null)
                return null;

            foreach (var endereco in enderecosPrestadores)
            {
                var prestador = _prestadoresRepository.Get(endereco.PrestadorId);

                if (prestador is not null)
                    prestadoresList.Add(prestador);
            }

            return prestadoresList;

        }
    }
}
