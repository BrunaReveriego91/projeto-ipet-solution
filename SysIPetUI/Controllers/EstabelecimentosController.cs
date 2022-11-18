using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysIPetUI.Models;

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
                var usuarioId = User.GetIdUsuario();
                
                HttpResponseMessage? response = await cliente.GetAsync(url + '/' + usuarioId);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                EstabelecimentosLists model = new EstabelecimentosLists();

                List<EstabelecimentoViewModel>? estabelecimentos = new List<EstabelecimentoViewModel>();
                estabelecimentos = JsonConvert.DeserializeObject<List<EstabelecimentoViewModel>>(responseBody);

                if (estabelecimentos != null)
                {
                    model.EstabelecimentosList = estabelecimentos;

                    return View(model);

                }

                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}
