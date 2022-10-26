using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Model;

namespace Pet.WebAPI.Interfaces.Controllers
{
    public interface IPrestadoresController
    {
        IActionResult GetPrestador(int id);
        Task<IActionResult> PostPrestador([FromBody] NovoPrestador prestador);
        Task<IActionResult> PutPrestador(int id, AlterarPrestador prestador);
        IActionResult DeletePrestador(int id);
        IActionResult GetAllPrestadores();
        IActionResult GetAgendamentosPrestador(int prestador_id);
    }
}
