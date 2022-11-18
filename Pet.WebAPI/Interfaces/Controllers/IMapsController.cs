using Microsoft.AspNetCore.Mvc;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IMapsController
    {
        Task<IActionResult> GetPrestadoresByUserLocation(string userId);
    }
}
