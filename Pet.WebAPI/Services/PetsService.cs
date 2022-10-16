using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class PetsService : IPetsService
    {
        private IPetsRepository _petsRepository;

        public PetsService(IPetsRepository petsRepository)
        {
            _petsRepository = petsRepository;
        }

        public async Task<Pets> Add(NovoPet novoPet)
        {
            var pet = new Pets()
            {
                ClienteId = novoPet.ClienteId,
                NomeCompleto = novoPet.NomeCompleto,
                TipoPet = novoPet.TipoPet,
                TamanhoPet = novoPet.TamanhoPet,
                Peso = novoPet.Peso,
                Genero = novoPet.Genero,
                Cor = novoPet.Cor,
                DataNascimento = novoPet.DataNascimento,
                Raca = novoPet.Raca
            };


            return await _petsRepository.Add(pet);
        }

        public async Task Delete(int id)
        {
            var entry = _petsRepository.Get(id);

            //Bruna, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (entry is null)
            //{
            //    throw new Exception($"Pet não encontrado pelo Id {id}.");
            //}
            await _petsRepository.Delete(entry);
        }

        public Pets? Get(int id)
        {
            return _petsRepository.Get(id);
        }

        public IEnumerable<Pets> GetPets()
        {
            return _petsRepository.GetAll();
        }

        public async Task Update(int id, AlterarPet pet)
        {
            var entry = _petsRepository.Get(id);

            //Bruna, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (entry is null)
            //{
            //    throw new Exception($"Pet não encontrado pelo Id {id}.");
            //}

            entry.NomeCompleto = pet.NomeCompleto;
            entry.ClienteId = pet.ClienteId;
            entry.DataNascimento = pet.DataNascimento;
            entry.Peso = pet.Peso;

            //Bruna, adicionei os demais campos pois o usuário pode precisar alterar
            entry.TipoPet = pet.TipoPet;
            entry.TamanhoPet = pet.TamanhoPet;
            entry.Genero = pet.Genero;
            entry.Cor = pet.Cor;
            entry.Raca = pet.Raca;


            try
            {
                await _petsRepository.Update(entry);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
