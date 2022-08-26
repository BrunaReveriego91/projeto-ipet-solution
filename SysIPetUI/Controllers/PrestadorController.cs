using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Net.Http.Json;
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
            try
            {
                PrestadorViewModel? prestadorIncluido = new PrestadorViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Como a API precisará dos novos dados do prestador no formato JSON, estamos serializando os dados
                    //da ViewModel PrestadorViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(prestador), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Prestador na Tabela do SQL
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
        [HttpGet]
        public async Task<IActionResult> EditPrestador(int Id)
        {
            PrestadorViewModel? prestador = new PrestadorViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Prestador que será Editado pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Prestador que será Editado.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    prestador = JsonConvert.DeserializeObject<PrestadorViewModel>(responseBody);
                }
            }
            return View(prestador);
        }

        // POST: PrestadorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPrestador(PrestadorViewModel prestador)
        {
            try
            {
                PrestadorViewModel? prestadorRecebido = new PrestadorViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Aqui realizamos o PutAsync que Utiliza a Pet.WebAPI para Editar o Prestador na Tabela do SQL usando o Id
                    using (var response = await httpClient.PutAsJsonAsync($"{url}/{prestador.Id}", prestador))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        prestadorRecebido = JsonConvert.DeserializeObject<PrestadorViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: PrestadorController/ExcluirPrestador/5
        public async Task<IActionResult> ExcluirPrestador(int Id)
        {
            PrestadorViewModel? prestador = new PrestadorViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Prestador que será excluído pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Prestador que será excluído.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    prestador = JsonConvert.DeserializeObject<PrestadorViewModel>(responseBody);
                }
            }
            return View(prestador);
        }

        // POST: PrestadorController/ExcluirPrestador/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirPrestador(int Id, IFormCollection form)
        {
            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o DeletAsync que Utiliza a Pet.WebAPI para Deletar o Prestador na Tabela do SQL usando o Id
                using (var response = await httpClient.DeleteAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
