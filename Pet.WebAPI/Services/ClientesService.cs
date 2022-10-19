﻿using Microsoft.EntityFrameworkCore;
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
                DataNascimento = Convert.ToDateTime(clientPet.DataNascimento),
                Telefone1 = clientPet.Telefone1,
                WhatsApp = clientPet.WhatsApp,
                Telefone2 = clientPet.Telefone2
            };

            var response = await _clientPetRepository.Add(cliente);

            //Bruna, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (response.Id == 0)
            //    throw new Exception($"Erro ao adicionar novo cliente.");


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

        public void Delete(int id)
        {
            var entry = _clientPetRepository.Get(id);

            //Bruna, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (entry is null)
            //{
            //    throw new Exception($"Cliente não encontrado pelo Id {id}.");
            //}

            _clientPetRepository.Delete(entry);

            if (entry.Endereco != null)
                _enderecoClienteRepository.Delete(entry.Endereco);
        }

        public Cliente? Get(int id)
        {
            var cliente = _clientPetRepository.Get(id);

            //Bruna, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (cliente is null)
            //    throw new Exception($"Cliente não encontrado pelo Id {id}.");

            return cliente;
        }

        public Cliente? Get(string userName)
        {
            var cliente = _clientPetRepository.GetByUserName(userName);
            return cliente;
        }

        public IEnumerable<Cliente> GetClientes()
        {
            var clientes = _clientPetRepository.GetAll();

            //Bruna, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if(clientes is null)
            //    throw new Exception($"Não há clientes cadastrados na base.");

            return clientes;

        }

        public async Task Update(int id, AlterarCliente clientPet)
        {
            var cliente = _clientPetRepository.Get(id);

            //Bruna, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (cliente is null)
            //{
            //    throw new Exception($"Cliente não encontrado pelo Id {id}.");
            //}

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
