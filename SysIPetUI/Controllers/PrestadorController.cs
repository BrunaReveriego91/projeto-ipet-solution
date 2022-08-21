using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Text;

namespace SysIPetUI.Controllers
{
    //[Authorize]
    public class PrestadorController : Controller
    {
        // Pegando o endereço com HttpClient
        //private readonly string url = "https://localhost:7271/api/Prestadores";
        private readonly string url = "https://localhost:44321/api/Prestadores";

        // GET: PrestadorController
        public async Task<IActionResult> Index()
        {
            var cliente = new HttpClient();

            try
            {
                // Recebendo as informações da API
                // HttpResponseMensage? Trata se o Retorno for Vazio
                // await realiza as consultas varias vezes se necessário
                HttpResponseMessage? response = await cliente.GetAsync(url);

                // EnsureSuccessStatusCode Trata os erros:
                // Nesse caso a Aplicação se vira sem um tratamento de erro personalizado
                response.EnsureSuccessStatusCode();

                // Obtendo os dados
                // Estatus 200 conseguiu fazer a solicitação e tras os dados serializados em formato texto
                string responseBody = await response.Content.ReadAsStringAsync();

                // Criando a Lista e Deserializando o Arquivo Json
                // O Interrogação trata os nulls                
                List<PrestadorViewModel>? listaPrestadores = new List<PrestadorViewModel>();
                listaPrestadores = JsonConvert.DeserializeObject<List<PrestadorViewModel>>(responseBody);

                return View(listaPrestadores);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: PrestadorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrestadorController/Create
        public ActionResult CreatePrestador()
        {
            return View();
        }

        // POST: PrestadorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePrestador(PrestadorViewModel prestador)
        {
            PrestadorViewModel? prestadorIncluido = new PrestadorViewModel();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(prestador), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        prestadorIncluido = JsonConvert.DeserializeObject<PrestadorViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // GET: PrestadorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrestadorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrestadorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrestadorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
