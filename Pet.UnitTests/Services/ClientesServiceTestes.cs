using NSubstitute;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Services;
using Xunit;

namespace Pet.UnitTests.Services
{
    public class ClientesServiceTestes
    {
        private readonly IClientesRepository _clienteRepository;

        public ClientesServiceTestes()
        {
            _clienteRepository = Substitute.For<IClientesRepository>();
        }

        private ClientesService CriaServico()
        {
            return new ClientesService(_clienteRepository);
        }

        [Fact]
        public void RetornarClientePeloId()
        {
            //Arrange
            var service = CriaServico();
            //Act
            var cliente = new Cliente() { Id = 1, NomeCompleto = "Fulano de Tal", Aniversario = "01/01/1001", CPF = "123.456.789-10", WhatsApp = true, Telefone1 = "(11)99999-9999", Telefone2 = "(22)88888-8888" };
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
            var cliente = new Cliente() { Id = 1, NomeCompleto = "Fulano de Tal", Aniversario = "01/01/1001", CPF = "123.456.789-10", WhatsApp = true, Telefone1 = "(11)99999-9999", Telefone2 = "(22)88888-8888" };

         
            _clienteRepository.Add(Arg.Any<Cliente>()).Returns(Task.FromResult<Cliente>(cliente));

            var novoCliente = new NovoCliente()
            {
                NomeCompleto = cliente.NomeCompleto,
                CPF = cliente.CPF,
                Aniversario = cliente.Aniversario,
                Telefone1 = cliente.Telefone1,
                WhatsApp = cliente.WhatsApp,
                Telefone2 = cliente.Telefone2
            };


            var resultado = await service.Add(novoCliente);

            //Assert
            Assert.NotNull(resultado);
            Assert.IsType<Cliente>(resultado);
        }

    }
}
