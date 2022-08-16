using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarController : Controller, IRegistrarController
    {
    }

    internal interface IRegistrarController
    {
    }
}
