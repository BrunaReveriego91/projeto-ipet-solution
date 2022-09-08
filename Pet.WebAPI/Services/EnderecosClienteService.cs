using Pet.WebAPI.Repositories;

namespace Pet.WebAPI.Services
{
    public class EnderecosClienteService /*: IEnderecosClienteService*/
    {
        private readonly EnderecosClienteRepository _enderecosClienteRepository;

        public EnderecosClienteService(EnderecosClienteRepository enderecosClienteRepository)
        {
            _enderecosClienteRepository = enderecosClienteRepository;
        }


    }
}
