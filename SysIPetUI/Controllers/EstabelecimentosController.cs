using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Security.Claims;
using static SysIPetUI.Models.EstabelecimentoViewModel;

namespace SysIPetUI.Controllers
{
    public class EstabelecimentosController : Controller
    {
        private readonly string url = "https://localhost:44321/api/Maps";

        public async Task<IActionResult> Index()
        {

            var cliente = new HttpClient();

            try
            {
                var userName = User.GetUserName();
                int idUsuario = 1;

                HttpResponseMessage? response = await cliente.GetAsync(url + '/' + idUsuario);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                                   
                List<EstabelecimentoViewModel>? estabelecimentos = new List<EstabelecimentoViewModel>();
                estabelecimentos = JsonConvert.DeserializeObject<List<EstabelecimentoViewModel>>(responseBody);

                if (estabelecimentos != null)
                {
                    return View(estabelecimentos);

                }

                return View("Error");
                //int id = 15;

                //HttpResponseMessage? response = await cliente.GetAsync(url + '/' + id);

                //response.EnsureSuccessStatusCode();

                //string responseBody = await response.Content.ReadAsStringAsync(); 
                //List<PrestadorViewModel>? listaPrestadores = new List<PrestadorViewModel>();
                //listaPrestadores = JsonConvert.DeserializeObject<List<PrestadorViewModel>>(responseBody);

                //ViewData["Prestadores"] = listaPrestadores.ToArray();

                //return View(listaPrestadores);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}
