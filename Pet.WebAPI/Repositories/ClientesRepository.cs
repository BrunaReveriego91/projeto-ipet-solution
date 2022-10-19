using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class ClientesRepository : BaseRepository<Cliente, PetContext>, IClientesRepository
    {
        public ClientesRepository(PetContext context) : base(context)
        {
        }

        public override Cliente? Get(int id)
        {
            var cliente = (from p in DataContext.Clientes
                           where p.Id == id
                           select p).FirstOrDefault();

            if (cliente == null)
                return null;

            var enderecoCliente = (from e in DataContext.EnderecosClientes
                                   where e.ClienteId == id
                                   select e).FirstOrDefault();

            cliente.Endereco = enderecoCliente;

            return cliente;
        }

        //public override async Task Delete(Cliente cliente)
        //{
        //    await base.Delete(cliente);
        //}

        public override IEnumerable<Cliente> GetAll()
        {
            var clientes = (from p in DataContext.Clientes
                            select p).ToList();

            if (clientes.Count == 0 || clientes == null)
                return null;


            var clienteList = new List<Cliente>();

            foreach (var cliente in clientes)
            {
                var enderecoCliente = (from e in DataContext.EnderecosClientes
                                       where e.ClienteId == cliente.Id
                                       select e).FirstOrDefault();

                if (enderecoCliente != null)
                    cliente.Endereco = enderecoCliente;

                clienteList.Add(cliente);
            }

            return clienteList;
        }

        public override Task Update(Cliente cliente)
        {
            return base.Update(cliente);
        }
    }
}
