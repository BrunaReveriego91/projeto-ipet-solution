using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class ServicosService : IServicosServices
    {
        private readonly IServicosRepository _repository;

        public ServicosService(IServicosRepository servicosRepository)
        {
            _repository = servicosRepository;
        }

        public async Task<Servico> Add(NovoServico servico)
        {
            return await _repository.Add(new Servico(servico.Nome, servico.Descricao, servico.Ativo));
        }

        public void Delete(int id)
        {
            var entry = _repository.Get(id);
            
            if (entry is null) 
            { 
                return;
            }

            _repository.Delete(entry);
        }

        public Servico? Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Servico>? GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public async Task Update(int id, AlterarServico servico)
        {
            var entry = _repository.Get(id);

            //Eberton, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (entry is null)
            //{
            //    throw new Exception($"Serviço não encontrado pelo Id {id}.");
            //}

            entry.Nome = servico.Nome;
            entry.Descricao = servico.Descricao;
            entry.Ativo = servico.Ativo;

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
