using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Interfaces.Repositories;

namespace Pet.WebAPI.Repositories
{
    public class ClientPetRepository : IClientPetRepository, IDisposable
    {
        private PetContext context;
        public ClientPetRepository()
        {
            this.context = new PetContext();
        }
        public async Task Add(ClientPet clientPet)
        {

            context.Add(clientPet);
            await context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            ClientPet clientPet = new ClientPet() { Id = id };
            context.Pets.Attach(clientPet);
            context.Pets.Remove(clientPet);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<List<ClientPet>> ListClientPets()
        {
            return await Task.Run(() => context.Pets.ToList());
        }

        public async Task Update(ClientPet clientPet)
        {
            var clientPetObj = context.Pets.Where(x => 
            x.Owner == clientPet.Owner &&
            x.Name == clientPet.Name
            ).FirstOrDefault();

            if(clientPetObj != null)
            {
                context.Pets.Update(clientPetObj);
                await context.SaveChangesAsync();
            }
        }
    }
}
