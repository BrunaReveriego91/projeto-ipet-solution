﻿using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Repositories;
using System.Linq.Expressions;

namespace Pet.WebAPI.Services
{
    public class EnderecosPrestadorService : IEnderecosPrestadorService
    {
        private readonly IEnderecosPrestadorRepository _repository;

        public EnderecosPrestadorService(IEnderecosPrestadorRepository repository)
        {
            _repository = repository;
        }

        public async Task Add(NovoEnderecoPrestador novoEndereco)
        {
            var endereco = new EnderecoPrestador()
            {
                PrestadorId = novoEndereco.PrestadorId,
                Bairro = novoEndereco.Bairro,
                CEP = novoEndereco.CEP,
                Cidade = novoEndereco.Cidade,
                Complemento = novoEndereco.Complemento,
                Logradouro = novoEndereco.Logradouro,
                Numero = novoEndereco.Numero,
                Referencia = novoEndereco.Referencia,
                SemNumero = novoEndereco.SemNumero,
                UF = novoEndereco.UF
            };

            await _repository.Add(endereco);
        }

        public async Task Delete(int id)
        {
            var entry = _repository.Get(id);
            if (entry == null) return;
            await _repository.Delete(entry);
        }

        public List<EnderecoPrestador>? GetAll(int prestador_id)
        {
            return _repository.GetAll(p => p.PrestadorId == prestador_id).ToList();
        }

        public async Task Update(int id, AlterarEnderecoPrestador endereco)
        {
            var entry = _repository.Get(id);

            if (entry is null)
            {
                throw new Exception($"Endereco Prestador não encontrado pelo Id {id}.");
            }

            entry.CEP = endereco.CEP;   
            entry.UF = endereco.UF;
            entry.Bairro = endereco.Bairro;
            entry.Cidade = endereco.Cidade;
            entry.Complemento = endereco.Complemento;
            entry.Logradouro = endereco.Logradouro;
            entry.Numero = endereco.Numero;
            entry.Referencia = endereco.Referencia;
            entry.SemNumero = endereco.SemNumero;

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

    public interface IEnderecosPrestadorService
    {
        Task Add(NovoEnderecoPrestador novo);

        /// <summary>
        /// Obtém todos os endereços do Prestador.
        /// </summary>
        /// <param name="prestador_id"></param>
        /// <returns></returns>
        List<EnderecoPrestador>? GetAll(int prestador_id);

        Task Update(int id, AlterarEnderecoPrestador endereco);
        Task Delete(int id);
    }
}