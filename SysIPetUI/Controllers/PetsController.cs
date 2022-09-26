using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Diagnostics;
using System.Text;

namespace SysIPetUI.Controllers
{
    [Authorize]
    public class PetsController : Controller
    {
        // Pegando o endereço com HttpClient        
        private readonly string url = "https://localhost:44321/api/Pets";

        // GET: PetController
        public async Task<IActionResult> Index()
        {
            var pet = new HttpClient();

            try
            {
                // Recebendo as informações da API
                // HttpResponseMensage? Trata se o Retorno for Vazio
                // await realiza as consultas varias vezes se necessário
                HttpResponseMessage? response = await pet.GetAsync(url);

                // EnsureSuccessStatusCode Trata os erros:
                // Nesse caso a Aplicação se vira sem um tratamento de erro personalizado
                response.EnsureSuccessStatusCode();

                // Obtendo os dados
                // Estatus 200 conseguiu fazer a solicitação e tras os dados serializados em formato texto
                string responseBody = await response.Content.ReadAsStringAsync();

                // Criando a Lista e Deserializando o Arquivo Json
                // O Interrogação trata os nulls                
                List<PetsViewModel>? listaPets = new List<PetsViewModel>();
                listaPets = JsonConvert.DeserializeObject<List<PetsViewModel>>(responseBody);

                return View(listaPets);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: PetController/Create
        public ActionResult CreatePet()
        {
            return View();
        }

        // POST: PetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePet(PetsViewModel pet)
        {
            try
            {
                PetsViewModel? petIncluido = new PetsViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Como a API precisará dos novos dados do Pet no formato JSON, estamos serializando os dados
                    //da ViewModel PetsViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Pet na Tabela do SQL
                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        petIncluido = JsonConvert.DeserializeObject<PetsViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // GET: PetController/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditPet(int Id)
        {
            PetsViewModel? pet = new PetsViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Pet que será Editado pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Pet que será Editado.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    pet = JsonConvert.DeserializeObject<PetsViewModel>(responseBody);
                }
            }
            return View(pet);
        }

        // POST: PetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPet(PetsViewModel pet)
        {
            try
            {
                PetsViewModel? petRecebido = new PetsViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Aqui realizamos o PutAsync que Utiliza a Pet.WebAPI para Editar o Pet na Tabela do SQL usando o Id
                    using (var response = await httpClient.PutAsJsonAsync($"{url}/{pet.Id}", pet))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        petRecebido = JsonConvert.DeserializeObject<PetsViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: PetController/ExcluirPet/5
        public async Task<IActionResult> ExcluirPet(int Id)
        {
            PetsViewModel? pet = new PetsViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Pet que será excluído pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Pet que será excluído.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    pet = JsonConvert.DeserializeObject<PetsViewModel>(responseBody);
                }
            }
            return View(pet);
        }

        // POST: PetController/ExcluirPet/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirPet(int Id, IFormCollection form)
        {
            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o DeletAsync que Utiliza a Pet.WebAPI para Deletar o Pet na Tabela do SQL usando o Id
                using (var response = await httpClient.DeleteAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
