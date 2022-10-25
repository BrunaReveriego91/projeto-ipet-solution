using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class PrestadoresService : IPrestadoresService
    {
        private readonly IPrestadoresRepository _repository;

        public PrestadoresService(IPrestadoresRepository repository)
        {
            _repository = repository;
        }

        public async Task<Prestador> Add(NovoPrestador novoPrestador)
        {
            var prestador = new Prestador()
            {
                Id_Prestador = novoPrestador.Id_Prestador,
                NomeCompleto = novoPrestador.NomeCompleto,
                CPF_CNPJ = novoPrestador.CPF_CNPJ,
                Telefone = novoPrestador.Telefone,
                WhatsApp = novoPrestador.WhatsApp
            };
            return await _repository.Add(prestador);
        }

        public void Delete(int id)
        {
            var entry = _repository.Get(id);

            if (entry is null)
            {
                throw new Exception($"Prestador não encontrado pelo Id {id}.");
            }

            _repository.Delete(entry);
        }

        public Prestador? Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Agenda> GetAgendamentosPrestador(int prestador_id)
        {
            return _repository.GetAgendamentosPrestador(prestador_id);
        }

        public IEnumerable<Prestador> GetAllPrestadores()
        {
            return _repository.GetAll();
        }

        public async Task Update(int id, AlterarPrestador entity)
        {
            var entry = _repository.Get(id);

            //Eberton, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (entry is null)
            //{
            //    throw new Exception($"Prestador não encontrado pelo Id {id}.");
            //}

            entry.NomeCompleto = entity.NomeCompleto;
            entry.CPF_CNPJ = entity.CPF_CNPJ;
            entry.Telefone = entity.Telefone;
            entry.WhatsApp = entity.WhatsApp;

            try
            {
                await _repository.Update(entry);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }

}
