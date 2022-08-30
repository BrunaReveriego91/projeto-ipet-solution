using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Interfaces.Controllers;

namespace Pet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class AgendamentoController : Controller, IAgendamentoController
    {
    }
    
}
