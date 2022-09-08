using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysIPetUI.Models;

namespace SysIPetUI.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly string url = "https://localhost:44321/api/Agendamento";

        // GET: Agendamento
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
                List<AgendamentoViewModel>? listaAgendamentos = new List<AgendamentoViewModel>();
                listaAgendamentos = JsonConvert.DeserializeObject<List<AgendamentoViewModel>>(responseBody);

                return View(listaAgendamentos);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Agendamento/Details/5
        public IActionResult Details(int? id)
        {            
            return View();
        }

        // GET: Agendamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agendamento/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgendamentoViewModel agendamento)
        {
            try
            {
                AgendamentoViewModel? agendamentoIncluido = new AgendamentoViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Como a API precisará dos novos dados do agendamento no formato JSON, estamos serializando os dados
                    //da ViewModel AgendamentoViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(agendamento), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Agendamento na Tabela do SQL
                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        agendamentoIncluido = JsonConvert.DeserializeObject<AgendamentoViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Agendamento/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            AgendamentoViewModel? agendamento = new AgendamentoViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Agendamento que será Editado pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Agendamento que será Editado.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    agendamento = JsonConvert.DeserializeObject<AgendamentoViewModel>(responseBody);
                }
            }
            return View(agendamento);
        }

        // POST: Agendamento/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AgendamentoViewModel agendamento)
        {
            try
            {
                AgendamentoViewModel? agendamentoRecebido = new AgendamentoViewModel();

                using (var httpClient = new HttpClient())
                {
                    //Aqui realizamos o PutAsync que Utiliza a Pet.WebAPI para Editar o Agendamento na Tabela do SQL usando o Id
                    using (var response = await httpClient.PutAsJsonAsync($"{url}/{agendamento.Id}", agendamento))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        agendamentoRecebido = JsonConvert.DeserializeObject<AgendamentoViewModel>(responseBody);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Agendamento/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {
            AgendamentoViewModel? agendamento = new AgendamentoViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Agendamento que será excluído pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Agendamento que será excluído.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    agendamento = JsonConvert.DeserializeObject<AgendamentoViewModel>(responseBody);
                }
            }
            return View(agendamento);
        }

        // POST: Agendamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o DeletAsync que Utiliza a Pet.WebAPI para Deletar o Agendamento na Tabela do SQL usando o Id
                using (var response = await httpClient.DeleteAsync(url + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }        
    }
}
