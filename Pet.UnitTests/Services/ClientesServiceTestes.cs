using NSubstitute;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;
using Pet.WebAPI.Services;
using Xunit;

namespace Pet.UnitTests.Services
{
    public class ClientesServiceTestes
    {
        private readonly IClientesRepository _clienteRepository;
        private readonly IEnderecosClienteRepository _enderecoClienteRepository;
        private readonly IClientesService _clientesService;
        private readonly IEnderecosClienteService _enderecosClienteService;

        public ClientesServiceTestes()
        {
            _clienteRepository = Substitute.For<IClientesRepository>();
            _enderecoClienteRepository = Substitute.For<IEnderecosClienteRepository>();
            _clientesService = Substitute.For<IClientesService>();
            _enderecosClienteService = Substitute.For<IEnderecosClienteService>();
        }

        private ClientesService CriaServico()
        {
            return new ClientesService(_clienteRepository, _enderecoClienteRepository);
        }

        [Fact]
        public void RetornarClientePeloId()
        {
            //Arrange
            var service = CriaServico();
            //Act
            var enderecoCliente = new EnderecoCliente() { Logradouro = "Rua do Teste", Bairro = "Vila do Teste", CEP = "0000-000", Cidade = "São Paulo", Numero = 1, ClienteId = 1, Complemento = "N/A", Data_Cadastro = new System.DateTime(2015, 3, 10), Referencia = "Esquina do Teste", SemNumero = false, UF = "SP" };

            var cliente = new Cliente()
            {
                Id = 1,
                NomeCompleto = "Fulano de Tal",
                CPF = "123.456.789-10",
                DataNascimento = new System.DateTime(2016, 01, 01),
                Telefone1 = "(11)99999-9999",
                WhatsApp = true,
                Telefone2 = "(22)88888-8888",
                Endereco = enderecoCliente,
            };

            _clienteRepository.Get(1).Returns(cliente);

            var resultado = service.Get(1);

            //Assert
            Assert.NotNull(resultado);
            Assert.IsType<Cliente>(resultado);
        }

        [Fact]
        public async Task InserirNovoClienteAsync()
        {
            //Arrange
            var service = CriaServico();
            //Act
            var enderecoCliente = new EnderecoCliente() { Logradouro = "Rua do Teste", Bairro = "Vila do Teste", CEP = "0000-000", Cidade = "São Paulo", Numero = 1, ClienteId = 1, Complemento = "N/A", Data_Cadastro = new System.DateTime(2015, 3, 10), Referencia = "Esquina do Teste", SemNumero = false, UF = "SP" };

            var cliente = new Cliente()
            {
                NomeCompleto = "Fulano de Tal",
                CPF = "123.456.789-10",
                DataNascimento = new System.DateTime(2016, 01, 01),
                Telefone1 = "(11)99999-9999",
                WhatsApp = true,
                Telefone2 = "(22)88888-8888",
                Endereco = enderecoCliente,
                Id = 1,
                Data_Cadastro = DateTime.Now,
                IdUsuario = "1"
            };

            _clienteRepository.Add(Arg.Any<Cliente>()).Returns(Task.FromResult<Cliente>(cliente));

            //var novoCliente = new NovoCliente(cliente.NomeCompleto, cliente.CPF, cliente.DataNascimento, cliente.Telefone1, cliente.WhatsApp, cliente.Telefone2, enderecoCliente);
            var novoCliente = new NovoCliente()
            {
                IdUsuario = cliente.IdUsuario,
                WhatsApp = cliente.WhatsApp,
                NomeCompleto = cliente.NomeCompleto,
                CPF = cliente.CPF,
                DataNascimento = cliente.DataNascimento.GetValueOrDefault().ToShortDateString(),
                Telefone1 = cliente.Telefone1,
                Telefone2 = cliente.Telefone2,
                Endereco = new NovoCliente.EnderecoNovoCliente()
                {
                    Logradouro = enderecoCliente.Logradouro,
                    Bairro = enderecoCliente.Bairro,
                    CEP = enderecoCliente.CEP,
                    Cidade = enderecoCliente.Cidade,
                    Numero = enderecoCliente.Numero,
                    Complemento = "",
                    Referencia = "",
                    SemNumero = false,
                    UF = "SP"
                }
            };

            var resultado = await service.Add(novoCliente);

            //Assert
            Assert.NotNull(resultado);
            Assert.IsType<Cliente>(resultado);
        }
    }
}