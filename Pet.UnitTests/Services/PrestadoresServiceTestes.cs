using NSubstitute;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
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
            var servicoPrestador = new ServicoPrestador() { Id = 1, PrestadorId = 1, ServicoId = 1, Ativo = true, Data_Cadastro = new System.DateTime(2015, 01, 01), Valor = 50.00f };

            var listEndereco = new List<EnderecoPrestador>() { enderecoPrestador };
            var listServico = new List<ServicoPrestador>() { servicoPrestador };


            var prestador = new Prestador() { Id = 1, NomeCompleto = "Fulano de Tal", CPF_CNPJ = "111.111.111-111", Data_Cadastro = new System.DateTime(2015, 3, 10), Telefone = "(11) 1234-5678", WhatsApp = true, Enderecos = listEndereco, Servicos = listServico };
            _prestadoresRepository.Get(1).Returns(prestador);

            var resultado = service.Get(1);

            //Assert
            Assert.NotNull(resultado);
            Assert.IsType<Prestador>(resultado);
        }

        [Fact]
        public async Task InserirNovoPrestadorAsync()
        {
            //Arrange
            var service = CriaServico();
            //Act
            var enderecoPrestador = new EnderecoPrestador() { Logradouro = "Rua do Teste", Bairro = "Vila do Teste", CEP = "0000-000", Cidade = "São Paulo", Numero = 1, PrestadorId = 1, Complemento = "N/A", Data_Cadastro = new System.DateTime(2015, 3, 10), Referencia = "Esquina do Teste", SemNumero = false, UF = "SP", Telefone = "(11)1234-56789", WhatsApp = true };
            var servicoPrestador = new ServicoPrestador() { Id = 1, PrestadorId = 1, ServicoId = 1, Ativo = true, Data_Cadastro = new System.DateTime(2015, 01, 01), Valor = 50.00f };

            var listEndereco = new List<EnderecoPrestador>() { enderecoPrestador };
            var listServico = new List<ServicoPrestador>() { servicoPrestador };


            var prestador = new Prestador() { Id = 1, NomeCompleto = "Fulano de Tal", CPF_CNPJ = "123.456.789-10", WhatsApp = true, Telefone = "(11)99999-9999", Data_Cadastro = new DateTime(2015, 01, 01), Enderecos = listEndereco, Servicos = listServico };


            _prestadoresRepository.Add(Arg.Any<Prestador>()).Returns(Task.FromResult<Prestador>(prestador));

            var novoPrestador = new NovoPrestador()
            {
                NomeCompleto = "Fulano de Tal",
                CPF_CNPJ = "123.456.789-10"
            };


            var resultado = await service.Add(novoPrestador);

            //Assert
            Assert.NotNull(resultado);
            Assert.IsType<Prestador>(resultado);
        }


    }
}
