using Microsoft.AspNetCore.Mvc;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IMapsController
    {
        IActionResult GetPrestadoresByUserLocation(int userId);
    }
}
