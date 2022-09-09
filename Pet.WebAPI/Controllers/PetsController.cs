using Microsoft.AspNetCore.Mvc;

namespace Pet.WebAPI.Controllers
{
    public class PetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
