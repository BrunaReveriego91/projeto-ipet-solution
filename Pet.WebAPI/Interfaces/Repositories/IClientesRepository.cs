﻿using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Interfaces.Repositories
{
    public interface IClientesRepository
    {
        Task<Cliente> Add(Cliente clientePet);
        Cliente? Get(int id);
        Task Update(Cliente clientPet);
        Task Delete(Cliente cliente);
        IEnumerable<Cliente> GetAll();
    }
}
