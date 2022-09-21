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
        private IEnderecosClienteRepository _enderecoClienteRepository;

        public ClientesService(IClientesRepository clientPetRepository, IEnderecosClienteRepository enderecoClienteRepository)
        {
            _clientPetRepository = clientPetRepository;
            _enderecoClienteRepository = enderecoClienteRepository;
        }

        public async Task<Cliente?> Add(NovoCliente clientPet)
        {
            var cliente = new Cliente()
            {
                NomeCompleto = clientPet.NomeCompleto,
                CPF = clientPet.CPF,
                DataNascimento = clientPet.DataNascimento,
                Telefone1 = clientPet.Telefone1,
                WhatsApp = clientPet.WhatsApp,
                Telefone2 = clientPet.Telefone2
            };

            var response = await _clientPetRepository.Add(cliente);
            if (response.Id == 0)
                return null;


            if (clientPet.Endereco != null)
            {
                var enderecoCliente = new EnderecoCliente()
                {
                    ClienteId = response.Id,
                    Logradouro = clientPet.Endereco.Logradouro,
                    Bairro = clientPet.Endereco.Bairro,
                    CEP = clientPet.Endereco.CEP,
                    Cidade = clientPet.Endereco.Cidade,
                    SemNumero = clientPet.Endereco.SemNumero,
                    Complemento = clientPet.Endereco.Complemento,
                    Data_Cadastro = clientPet.Endereco.Data_Cadastro,
                    Numero = clientPet.Endereco.Numero,
                    Referencia = clientPet.Endereco.Referencia,
                    UF = clientPet.Endereco.UF
                };

                await _enderecoClienteRepository.Add(enderecoCliente);
            }

            return _clientPetRepository.Get(response.Id);
        }

        public async Task Delete(int id)
        {
            var entry = _clientPetRepository.Get(id);
            var endereco = _enderecoClienteRepository.Get(entry.Endereco.Id);

            if (entry is null)
            {
                throw new Exception($"Cliente não encontrado pelo Id {id}.");
            }

            await _clientPetRepository.Delete(entry);

            if (endereco != null)
                await _enderecoClienteRepository.Delete(endereco);
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

            try
            {
                await _clientPetRepository.Update(cliente);

                if (cliente.Endereco != null)
                {
                    var endereco = _enderecoClienteRepository.Get(cliente.Endereco.Id);

                    endereco.Logradouro = clientPet.Endereco.Logradouro;
                    endereco.Bairro = clientPet.Endereco.Bairro;
                    endereco.CEP = clientPet.Endereco.CEP;
                    endereco.Cidade = clientPet.Endereco.Cidade;
                    endereco.SemNumero = clientPet.Endereco.SemNumero;
                    endereco.Complemento = clientPet.Endereco.Complemento;
                    endereco.Numero = clientPet.Endereco.Numero;
                    endereco.Referencia = clientPet.Endereco.Referencia;
                    endereco.UF = clientPet.Endereco.UF;

                    await _enderecoClienteRepository.Update(endereco);
                }
                else
                {
                    var enderecoCliente = new EnderecoCliente()
                    {
                        Logradouro = clientPet.Endereco.Logradouro,
                        Bairro = clientPet.Endereco.Bairro,
                        CEP = clientPet.Endereco.CEP,
                        Cidade = clientPet.Endereco.Cidade,
                        SemNumero = clientPet.Endereco.SemNumero,
                        Complemento = clientPet.Endereco.Complemento,
                        Numero = clientPet.Endereco.Numero,
                        Referencia = clientPet.Endereco.Referencia,
                        UF = clientPet.Endereco.UF,
                        ClienteId = cliente.Id,
                        Data_Cadastro = DateTime.Now
                    };

                    await _enderecoClienteRepository.Add(enderecoCliente);
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
