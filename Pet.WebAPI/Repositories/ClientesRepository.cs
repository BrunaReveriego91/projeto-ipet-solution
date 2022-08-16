using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class ClientesRepository : IClienteRepository, IDisposable
    {
        private readonly PetContext context;
        public ClientesRepository()
        {
            this.context = new PetContext();
        }
        public async Task Add(Cliente clientPet)
        {

            context.Add(clientPet);
            await context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            Cliente clientPet = new() { Id = id };
            context.Clientes?.Attach(clientPet);
            context.Clientes?.Remove(clientPet);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<List<Cliente>> ListClientPets()
        {
            return await Task.Run(() => context.Clientes.ToList());
        }

        public async Task Update(Cliente clientPet)
        {
            var clientPetObj = context.Clientes?.Where(x =>
            x.Id == clientPet.Id &&
            x.NomeCompleto == clientPet.NomeCompleto
            ).FirstOrDefault();

            if (clientPetObj != null)
            {
                context.Clientes?.Update(clientPetObj);
                await context.SaveChangesAsync();
            }
        }
    }
}
