using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class ClientesService : IClientesService
    {
        private IClientesRepository _clientPetRepository;

        public ClientesService(IClientesRepository clientPetRepository)
        {
            _clientPetRepository = clientPetRepository;
        }

        public async Task<Cliente> Add(NovoCliente clientPet)
        {
            var cliente = new Cliente()
            {
                NomeCompleto = clientPet.NomeCompleto,
                CPF = clientPet.CPF,
                DataNascimento = clientPet.DataNascimento,
                Telefone1 = clientPet.Telefone1,
                WhatsApp = clientPet.WhatsApp,
                Telefone2 = clientPet.Telefone2,
                Enderecos = clientPet.Enderecos
            };

            return await _clientPetRepository.Add(cliente);
        }

        public async Task Delete(int id)
        {
            var entry = _clientPetRepository.Get(id);

            if (entry is null)
            {
                throw new Exception($"Cliente não encontrado pelo Id {id}.");
            }
            await _clientPetRepository.Delete(entry);
        }

        public Cliente? Get(int id)
        {
            return _clientPetRepository.Get(id);
        }


        public IEnumerable<Cliente> GetClientes()
        {
            return _clientPetRepository.GetAll();
        }

        public async Task Update(int id, AlterarCliente clientPet)
        {
            var cliente = _clientPetRepository.Get(id);

            if (cliente is null)
            {
                throw new Exception($"Cliente não encontrado pelo Id {id}.");
            }

            cliente.NomeCompleto = clientPet.NomeCompleto;
            cliente.CPF = clientPet.CPF;
            cliente.DataNascimento = clientPet.DataNascimento;
            cliente.Telefone1 = clientPet.Telefone1;
            cliente.WhatsApp = clientPet.WhatsApp;
            cliente.Telefone2 = clientPet.Telefone2;
            cliente.Enderecos = clientPet.Enderecos;

            try
            {
                await _clientPetRepository.Update(cliente);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
