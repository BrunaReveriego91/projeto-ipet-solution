using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Text;

namespace SysIPetUI.Controllers
{
    public class ServicoController : Controller
    {      
        private readonly string urlServicos = "https://localhost:44321/api/Servicos";
        private readonly string urlServicosPrestador = "https://localhost:44321/api/ServicosPrestador";

        [HttpGet]
        public async Task<IActionResult> CadastroServicoPrestador()
        {
            //Criando uma nova Instância
            ServicoViewModel? viewModel = new ServicoViewModel();

            //Preenchendo as Listas            
            viewModel.ServicoList = GetServicosViewModel();            

            if (viewModel == null)
            {
                return RedirectToAction("CadastroPrestador");
            }

            return View(viewModel);
        }

        // POST: ServicoController/CadastroServicoPrestador
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroServicoPrestador(ServicoViewModel viewModel)
        {
            //Retorna somente os itens selecionados
            var servicoSelecionado = viewModel.ServicoList.Where(x => x.Ativo == true).ToList<ServicoListItem>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    //Cadastra o Serviço Prestado escolhido pelo Prestador
                    //Serializando os dados da ViewModel PrestadorViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(servicoSelecionado), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir o ServicoPrestador do Prestador na Tabela do SQL
                    using (var response = await httpClient.PostAsync(urlServicosPrestador, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                    }
                }
                //Se estiver nullo não cadastrou o Prestador Redireciona para o Cadastro
                return RedirectToAction("CadastroPrestador", "Prestador");
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View(viewModel); 
        }

        //Pegando o Id do Prestador
        public int GetIdPrestador()
        {
            //Obtem Id do Usuário logado
            var usuarioId = User.GetIdUsuario();

            //Encontra e faz a leitura do arquivo appsettings.json:
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            //Conexão com o LocalDB
            var stringConexao = configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(stringConexao);

            //Select na Tabela
            SqlCommand cmd = new SqlCommand("Select Id From Prestadores Where Id_Prestador = @IdUsuario", con);
            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdUsuario", usuarioId);

            //Executa a consulta e armazena o resultado            
            var result = Convert.ToInt32(cmd.ExecuteScalar());

            //Fecha a conexão
            con.Close();

            return result;
        }

        //Get ServicosViewModel
        public List<ServicoListItem> GetServicosViewModel()
        {
            //Encontra e faz a leitura do arquivo appsettings.json:
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            //Conexão com o LocalDB
            var stringConexao = configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(stringConexao);

            //Select na Tabela
            SqlCommand cmd = new SqlCommand
            (
                "SELECT Id" +
                ", Nome" +
                ", Descricao" +
                ", Ativo " +
                "FROM Servicos"
            , con);

            con.Open();            

            SqlDataReader idr = cmd.ExecuteReader();
            List<ServicoListItem> servicoListItem = new List<ServicoListItem>();

            var PrestadorId = GetIdPrestador();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    servicoListItem.Add(new ServicoListItem
                    {
                        Id = Convert.ToInt32(idr["Id"]),
                        Nome = Convert.ToString(idr["Nome"]),
                        Descricao = Convert.ToString(idr["Descricao"]),                        
                        Ativo = Convert.ToBoolean(idr["Ativo"]),
                        PrestadorId = Convert.ToInt32(PrestadorId),
                        ServicoId = Convert.ToInt32(idr["Id"]),
                    });
                }
            }
            con.Close();
            return servicoListItem;
        }

    }
}
