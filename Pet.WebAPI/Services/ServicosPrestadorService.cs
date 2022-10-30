using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;

namespace Pet.WebAPI.Services
{
    public class ServicosPrestadorService : IServicosPrestadorService
    {
        private readonly IServicosPrestadorRepository _repository;
        private readonly IPrestadoresRepository _prestadoresRepository;
        private readonly IServicosRepository _servicosRepository;

        public ServicosPrestadorService(IServicosPrestadorRepository repository, IPrestadoresRepository prestadoresRepository, IServicosRepository servicosRepository)
        {
            _repository = repository;
            _prestadoresRepository = prestadoresRepository;
            _servicosRepository = servicosRepository;
        }

        public async Task<List<ServicoPrestador>> Add(List<NovoServicoPrestador> novoServico)
        {
            List<ServicoPrestador> listaServicoPrestador = new List<ServicoPrestador>();
            // Verifica se o Prestador existe
            var prestador = _prestadoresRepository.Get(novoServico.FirstOrDefault().Prestador_Id);

            if (prestador is null)
            {
                throw new NullReferenceException($"Prestador não encontrado pelo Id {novoServico.FirstOrDefault().Prestador_Id}.");
            }

            // Verifica se o Serviço existe

            //TODO: Verificar se servico existe na lista 
            //Eberton estou comentando pois precisa ser refeito
            //var servico = _servicosRepository.Get(novoServico.FirstOrDefault().Servico_Id);

            //if (servico is null)
            //{
            //    throw new NullReferenceException($"Serviço não encontrado pelo Id {novoServico.FirstOrDefault().Servico_Id}.");
            //}


            try
            {
                try
                {
                    foreach (var servicoPrestador in novoServico)
                    {
                        var servico = _servicosRepository.Get(servicoPrestador.Servico_Id);

                        if (servico is null)
                        {
                            throw new NullReferenceException($"Serviço não encontrado pelo Id {servicoPrestador.Servico_Id}.");
                        }

                        var srv_prest = new ServicoPrestador()
                        {
                            Prestador = prestador,
                            Servico = servico,
                            PrestadorId = servicoPrestador.Prestador_Id,
                            ServicoId = servicoPrestador.Servico_Id,
                            Ativo = servicoPrestador.Ativo,
                            Valor = servicoPrestador.Valor
                        };
                        listaServicoPrestador.Add(srv_prest);            
                        await _repository.Add(srv_prest);
                    }

                    return listaServicoPrestador;
                }
                catch (DbUpdateException dbupdate)
                {
                    if (dbupdate.InnerException is null)
                    {
                        throw;
                    }
                    if (dbupdate.InnerException.GetType() == typeof(SqlException))
                    {
                        // Relança SqlException.
                        throw dbupdate.InnerException;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                var collection = sqlEx.Errors.GetEnumerator();
                while (collection.MoveNext())
                {
                    var erro = collection.Current as SqlError;
                    switch (erro.Number)
                    {
                        case 2601:
                            throw new Exception($"Serviço Id {novoServico.FirstOrDefault().Servico_Id} duplicado para o Prestador {novoServico.FirstOrDefault().Prestador_Id}.");

                        default:
                            throw;
                    }
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
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

        public List<ServicoPrestador>? GetAllFromPrestador(int prestador_id)
        {
            return _repository.GetAll(p => p.PrestadorId == prestador_id).ToList();
        }

        public async Task Update(int id, AlterarServicoPrestador servico)
        {
            var entry = _repository.Get(id);

            //Eberton, comentei todos os Ifs com throw new Exception que vc adicionou
            //Pode retornar nulo mesmo que redireciono para a pág de Create

            //if (entry is null)
            //{
            //    throw new Exception($"Serviço não encontrado pelo Id {id}.");
            //}

            entry.Ativo = servico.Ativo;
            entry.Valor = servico.Valor;

            try
            {
                await _repository.Update(entry);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}
