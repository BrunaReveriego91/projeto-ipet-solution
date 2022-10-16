using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Diagnostics;
using System.Text;

namespace SysIPetUI.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        // Pegando o endereço com HttpClient        
        private readonly string url = "https://localhost:44321/api/Cliente";

        // GET: ClienteController
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
                List<ClienteViewModel>? listaCliente = new List<ClienteViewModel>();
                listaCliente = JsonConvert.DeserializeObject<List<ClienteViewModel>>(responseBody);

                if(listaCliente == null) 
                {
                    return RedirectToAction("CadastroCliente");
                }

                return View(listaCliente);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }               

        // GET: ClienteController/Create
        public ActionResult CreateCliente()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCliente(ClienteViewModel cliente)
        {
            try
            {
                ClienteViewModel? clienteIncluido = new ClienteViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Como a API precisará dos novos dados do Cliente no formato JSON, estamos serializando os dados
                    //da ViewModel ClienteViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Cliente na Tabela do SQL
                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        clienteIncluido = JsonConvert.DeserializeObject<ClienteViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // GET: ClienteController/CadastroCliente
        public ActionResult CadastroCliente()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroCliente(ClienteViewModel cliente)
        {
            try
            {
                ClienteViewModel? clienteIncluido = new ClienteViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Como a API precisará dos novos dados do Cliente no formato JSON, estamos serializando os dados
                    //da ViewModel ClienteViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Cliente na Tabela do SQL
                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        clienteIncluido = JsonConvert.DeserializeObject<ClienteViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // GET: ClienteController/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditCliente(int Id)
        {
            ClienteViewModel? cliente = new ClienteViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Cliente que será Editado pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Cliente que será Editado.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    cliente = JsonConvert.DeserializeObject<ClienteViewModel>(responseBody);
                }
            }
            return View(cliente);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCliente(ClienteViewModel cliente)
        {
            try
            {
                ClienteViewModel? clienteRecebido = new ClienteViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Aqui realizamos o PutAsync que Utiliza a Pet.WebAPI para Editar o Cliente na Tabela do SQL usando o Id
                    using (var response = await httpClient.PutAsJsonAsync($"{url}/{cliente.Id}", cliente))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        clienteRecebido = JsonConvert.DeserializeObject<ClienteViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: ClienteController/ExcluirCliente/5
        public async Task<IActionResult> ExcluirCliente(int Id)
        {
            ClienteViewModel? cliente = new ClienteViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Cliente que será excluído pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Cliente que será excluído.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    cliente = JsonConvert.DeserializeObject<ClienteViewModel>(responseBody);
                }
            }
            return View(cliente);
        }

        // POST: ClienteController/ExcluirCliente/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirCliente(int Id, IFormCollection form)
        {
            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o DeletAsync que Utiliza a Pet.WebAPI para Deletar o Cliente na Tabela do SQL usando o Id
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
