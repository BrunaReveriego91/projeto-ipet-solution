﻿using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class ServicosService : IServicosServices
    {
        private readonly IServicosRepository _repository;
        private readonly IServicosPrestadorRepository _servicosPrestadorRepository;

        public ServicosService(IServicosRepository servicosRepository, IServicosPrestadorRepository servicosPrestadorRepository)
        {
            _repository = servicosRepository;
            _servicosPrestadorRepository = servicosPrestadorRepository; 
        }

        public async Task<Servico> Add(NovoServico servico)
        {
            var result = await _repository.Add(new Servico(servico.Nome, servico.Descricao, servico.Ativo));
            return result;
        }

        public async Task Delete(int id)
        {
            var entry = _repository.Get(id);
            if (entry is null) { return; }
            await _repository.Delete(entry);
        }

        public Servico? Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Servico>? GetAll()
        {
            return _repository.GetAll().ToList();
        }

        //public List<Servico>? GetAllFromPrestador(int prestador_id)
        //{
        //    var servicos = _servicosPrestadorRepository.GetAll(p => p.PrestadorId == prestador_id);
        //    return _repository.GetAll().ToList();
        //}

        public async Task Update(int id, AlterarServico servico)
        {
            var entry = _repository.Get(id);

            if (entry is null)
            {
                throw new Exception($"Serviço não encontrado pelo Id {id}.");
            }

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