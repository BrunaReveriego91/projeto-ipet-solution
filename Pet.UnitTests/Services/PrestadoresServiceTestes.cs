using NSubstitute;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;
using Pet.WebAPI.Services;
using Xunit;

namespace Pet.UnitTests.Services
{
    public class PrestadoresServiceTestes
    {
        private IPrestadoresRepository _prestadoresRepository;

        public PrestadoresServiceTestes()
        {
            _prestadoresRepository = Substitute.For<IPrestadoresRepository>();
        }

        private PrestadoresService CriaServico()
        {
            return new PrestadoresService(_prestadoresRepository);
        }

        [Fact]
        public void RetornarPrestadorPeloId()
        {
            //Arrange
            var service = CriaServico();
            //Act
            var enderecoPrestador = new EnderecoPrestador() { Logradouro = "Rua do Teste", Bairro = "Vila do Teste", CEP = "0000-000", Cidade = "São Paulo", Numero = 1, PrestadorId = 1, Complemento = "N/A", Data_Cadastro = new System.DateTime(2015, 3, 10), Referencia = "Esquina do Teste", SemNumero = false, UF = "SP", Telefone = "(11)1234-56789", WhatsApp = true };

            var listEndereco = new List<EnderecoPrestador>() { enderecoPrestador };

            var prestador = new Prestador() { Id = 1, NomeCompleto = "Fulano de Tal", CPF_CNPJ = "111.111.111-111", Data_Cadastro = new System.DateTime(2015, 3, 10), Telefone = "(11) 1234-5678", WhatsApp = true,Enderecos = listEndereco };
            _prestadoresRepository.Get(1).Returns(prestador);

            var resultado = service.Get(1);

            //Assert
            Assert.NotNull(resultado);
            Assert.IsType<Cliente>(resultado);
        }

        //[Fact]
        //public async Task InserirNovoClienteAsync()
        //{
        //    //Arrange
        //    var service = CriaServico();
        //    //Act
        //    var cliente = new Cliente() { Id = 1, NomeCompleto = "Fulano de Tal", Aniversario = "01/01/1001", CPF = "123.456.789-10", WhatsApp = true, Telefone1 = "(11)99999-9999", Telefone2 = "(22)88888-8888" };


        //    _clienteRepository.Add(Arg.Any<Cliente>()).Returns(Task.FromResult<Cliente>(cliente));

        //    var novoCliente = new NovoCliente()
        //    {
        //        NomeCompleto = cliente.NomeCompleto,
        //        CPF = cliente.CPF,
        //        Aniversario = cliente.Aniversario,
        //        Telefone1 = cliente.Telefone1,
        //        WhatsApp = cliente.WhatsApp,
        //        Telefone2 = cliente.Telefone2
        //    };


        //    var resultado = await service.Add(novoCliente);

        //    //Assert
        //    Assert.NotNull(resultado);
        //    Assert.IsType<Cliente>(resultado);
        //}


    }
}
