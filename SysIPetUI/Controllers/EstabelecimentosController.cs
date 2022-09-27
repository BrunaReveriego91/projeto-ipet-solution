using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Text;

namespace SysIPetUI.Controllers
{
    public class EstabelecimentosController : Controller
    {
        private readonly string url = "https://localhost:44321/api/Cliente";

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

                ViewData["Cliente"] = listaCliente.ToArray().FirstOrDefault();

                return View(listaCliente);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}
