using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Diagnostics;
using System.Text;

namespace SysIPetUI.Controllers
{
    [Authorize]
    public class EnderecoClienteController : Controller
    {
        // Pegando o endereço com HttpClient        
        private readonly string url = "https://localhost:44321/api/EnderecoCliente";
        private readonly string urlCliente = "https://localhost:44321/api/Cliente";

        // GET: EnderecoClienteController
        public async Task<IActionResult> Index(int? Id)
        {
            Id = 4;

            if (Id == null)
            {
                //var cliente = new HttpClient();                
                //HttpResponseMessage? response = await cliente.GetAsync(urlCliente);
                //response.EnsureSuccessStatusCode();
                //string responseBody = await response.Content.ReadAsStringAsync();
                //List<ClienteViewModel>? listaCliente = new List<ClienteViewModel>();
                //listaCliente = JsonConvert.DeserializeObject<List<ClienteViewModel>>(responseBody);

                //ClienteViewModel? cliente = new ClienteViewModel();

                //using (var httpClient = new HttpClient())
                //{
                //    using (var response = await httpClient.GetAsync(urlCliente))
                //    {
                //        string responseBody = await response.Content.ReadAsStringAsync();
                //        cliente = JsonConvert.DeserializeObject<ClienteViewModel>(responseBody);
                //    }
                //}                

                //if (cliente == null)
                //{
                //    return RedirectToAction("CadastroCliente");
                //}
                //else 
                //{
                //    return RedirectToAction("Index", new { Id = cliente.Id });
                //    //return View("Error");
                //}

                return RedirectToAction("CadastroEnderecoCliente");

            }                      

            var enderecoCliente = new HttpClient();

            try
            {                
                HttpResponseMessage? response = await enderecoCliente.GetAsync(url + "/" + Id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync(); 
                List<EnderecoClienteViewModel>? listaEnderecoCliente = new List<EnderecoClienteViewModel>();
                listaEnderecoCliente = JsonConvert.DeserializeObject<List<EnderecoClienteViewModel>>(responseBody);

                return View(listaEnderecoCliente);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: EnderecoCliente/Details/5
        public async Task<IActionResult> DetailsEnderecoCliente(int? Id)
        {
            EnderecoClienteViewModel? enderecoCliente = new EnderecoClienteViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o EnderecoCliente que será excluído pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do EnderecoCliente que será excluído.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    enderecoCliente = JsonConvert.DeserializeObject<EnderecoClienteViewModel>(responseBody);
                }
            }
            return View(enderecoCliente);
        }

        // GET: EnderecoClienteController/Create
        public ActionResult CreateEnderecoCliente()
        {
            return View();
        }

        // POST: EnderecoClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnderecoCliente(EnderecoClienteViewModel enderecoCliente)
        {
            try
            {
                EnderecoClienteViewModel? enderecoClienteIncluido = new EnderecoClienteViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Como a API precisará dos novos dados do EnderecoCliente no formato JSON, estamos serializando os dados
                    //da ViewModel EnderecoClienteViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(enderecoCliente), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo EnderecoCliente na Tabela do SQL
                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        enderecoClienteIncluido = JsonConvert.DeserializeObject<EnderecoClienteViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index", new { Id = enderecoClienteIncluido?.ClienteId });
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // GET: EnderecoClienteController/Create
        public ActionResult CadastroEnderecoCliente()
        {
            return View();
        }

        // POST: EnderecoClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroEnderecoCliente(EnderecoClienteViewModel enderecoCliente)
        {
            try
            {
                EnderecoClienteViewModel? enderecoClienteIncluido = new EnderecoClienteViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Como a API precisará dos novos dados do EnderecoCliente no formato JSON, estamos serializando os dados
                    //da ViewModel EnderecoClienteViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(enderecoCliente), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo EnderecoCliente na Tabela do SQL
                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        enderecoClienteIncluido = JsonConvert.DeserializeObject<EnderecoClienteViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index", new { Id = enderecoClienteIncluido?.ClienteId });
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // GET: EnderecoClienteController/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditEnderecoCliente(int Id)
        {
            EnderecoClienteViewModel? enderecoCliente = new EnderecoClienteViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o EnderecoCliente que será Editado pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do EnderecoCliente que será Editado.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    enderecoCliente = JsonConvert.DeserializeObject<EnderecoClienteViewModel>(responseBody);
                }
            }
            return View(enderecoCliente);
        }

        // POST: EnderecoClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEnderecoCliente(EnderecoClienteViewModel enderecoCliente)
        {
            try
            {
                EnderecoClienteViewModel? enderecoClienteRecebido = new EnderecoClienteViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Aqui realizamos o PutAsync que Utiliza a Pet.WebAPI para Editar o EnderecoCliente na Tabela do SQL usando o Id
                    using (var response = await httpClient.PutAsJsonAsync($"{url}/{enderecoCliente.Id}", enderecoCliente))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        enderecoClienteRecebido = JsonConvert.DeserializeObject<EnderecoClienteViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: EnderecoClienteController/ExcluirEnderecoCliente/5
        public async Task<IActionResult> ExcluirEnderecoCliente(int Id)
        {
            EnderecoClienteViewModel? enderecoCliente = new EnderecoClienteViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o EnderecoCliente que será excluído pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do EnderecoCliente que será excluído.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    enderecoCliente = JsonConvert.DeserializeObject<EnderecoClienteViewModel>(responseBody);
                }
            }
            return View(enderecoCliente);
        }

        // POST: EnderecoClienteController/ExcluirEnderecoCliente/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirEnderecoCliente(int Id, IFormCollection form)
        {
            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o DeletAsync que Utiliza a Pet.WebAPI para Deletar o EnderecoCliente na Tabela do SQL usando o Id
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
