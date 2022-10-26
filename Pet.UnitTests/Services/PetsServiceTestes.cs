using NSubstitute;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Services;
using Xunit;

namespace Pet.UnitTests.Services
{
    public class PetsServiceTestes
    {
        private readonly IPetsRepository _petsRepository;

        public PetsServiceTestes()
        {
            _petsRepository = Substitute.For<IPetsRepository>();
        }

        private PetsService CriaServico()
        {
            return new PetsService(_petsRepository);
        }

        [Fact]
        public void RetornarPetPeloId()
        {
            //Arrange
            var service = CriaServico();
            //Act

            var pet = new Pets()
            {
                NomeCompleto = "Lulu da Pomerania",
                ClienteId = 1,
                TipoPet = WebAPI.Domain.Entities.Enums.EnumTipoPet.Canino,
                TamanhoPet = WebAPI.Domain.Entities.Enums.EnumTamanhoPet.Mini,
                Peso = 2,
                Genero = WebAPI.Domain.Entities.Enums.EnumGenero.Feminino,
                Cor = "Marrom",
                DataNascimento = new System.DateTime(2016, 01, 01),
                Raca = "Lulu da Pomerania"
            };

            _petsRepository.Get(1).Returns(pet);

            var resultado = service.Get(1);

            //Assert
            Assert.NotNull(resultado);
            Assert.IsType<Pets>(resultado);
        }

        [Fact]
        public async Task InserirNovoPetAsync()
        {
            //Arrange
            var service = CriaServico();
            //Act
            var pet = new Pets()
            {
                NomeCompleto = "Lulu da Pomerania",
                ClienteId = 1,
                TipoPet = WebAPI.Domain.Entities.Enums.EnumTipoPet.Canino,
                TamanhoPet = WebAPI.Domain.Entities.Enums.EnumTamanhoPet.Mini,
                Peso = 2,
                Genero = WebAPI.Domain.Entities.Enums.EnumGenero.Feminino,
                Cor = "Marrom",
                DataNascimento = new System.DateTime(2016, 01, 01),
                Raca = "Lulu da Pomerania"
            };

            _petsRepository.Add(Arg.Any<Pets>()).Returns(Task.FromResult<Pets>(pet));

            var novoPet = new NovoPet(pet.ClienteId, pet.NomeCompleto, pet.TipoPet, pet.TamanhoPet, pet.Peso, pet.Genero, pet.Cor, pet.DataNascimento, pet.Raca);
        

            var resultado = await service.Add(novoPet);


            //Assert
            Assert.NotNull(resultado);
            Assert.IsType<Pets>(resultado);
        }
    }
}
