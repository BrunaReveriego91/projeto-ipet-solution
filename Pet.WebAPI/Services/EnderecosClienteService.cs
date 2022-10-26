using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;
using Pet.WebAPI.Repositories;

namespace Pet.WebAPI.Services
{
    public class EnderecosClienteService : IEnderecosClienteService
    {
        private readonly IEnderecosClienteRepository _enderecosClienteRepository;

        public EnderecosClienteService(IEnderecosClienteRepository enderecosClienteRepository)
        {
            _enderecosClienteRepository = enderecosClienteRepository;
        }

        public async Task<EnderecoCliente> Add(NovoEnderecoCliente novoEndereco)
        {
            var endereco = new EnderecoCliente()
            {
                ClienteId = novoEndereco.ClienteId,
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

            return await _enderecosClienteRepository.Add(endereco);
        }

        public EnderecoCliente? Get(int id)
        {
            return _enderecosClienteRepository.Get(id);
        }

        //public void Delete(int id)
        //{
        //    var entry = _enderecosClienteRepository.Get(id);

        //    if (entry == null) return;

        //    _enderecosClienteRepository.Delete(entry);
        //}

        //public List<EnderecoCliente>? GetAll(int cliente_id)
        //{
        //    return _enderecosClienteRepository.GetAll(p => p.ClienteId == cliente_id).ToList();
        //}

        public async Task Update(int id, AlterarEnderecoCliente endereco)
        {
            var entry = _enderecosClienteRepository.Get(id);

            //Bruna, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (entry is null)
            //{
            //    throw new Exception($"Endereco Cliente não encontrado pelo Id {id}.");
            //}

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
                await _enderecosClienteRepository.Update(entry);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
