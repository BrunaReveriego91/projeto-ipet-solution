using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IPetsController
    {
        IActionResult GetPet(int id);
        Task<IActionResult> PostPet([FromBody] NovoPet novoPet);
        Task<IActionResult> PutPet(int id, AlterarPet pet);
        IActionResult DeletePet(int id);
        IActionResult GetAllPets();
    }
}
