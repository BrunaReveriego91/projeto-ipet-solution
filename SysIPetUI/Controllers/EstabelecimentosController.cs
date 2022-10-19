using Microsoft.AspNetCore.Mvc;
using SysIPetUI.Models;
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
                EstabelecimentosLists model = new EstabelecimentosLists();
                var locations = new List<EstabelecimentoViewModel>()
                {
                new EstabelecimentoViewModel(1, "Teste","Teste", -23.594786699313218, -46.68442189606897),
                new EstabelecimentoViewModel(2, "Hyderabad","Hyderabad, Telengana", 17.387140, 78.491684),
                new EstabelecimentoViewModel(3, "Bengaluru","Bengaluru, Karnataka", 12.972442, 77.580643)
                };
                model.EstabelecimentosList = locations;

                return View(model);

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
