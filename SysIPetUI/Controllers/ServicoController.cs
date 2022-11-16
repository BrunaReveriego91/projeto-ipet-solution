using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Text;

namespace SysIPetUI.Controllers
{
    public class ServicoController : Controller
    {
        private readonly string urlServicosPrestador = "https://localhost:44321/api/ServicosPrestador";
        
        [HttpGet]
        public ActionResult ServicoPrestador()
        {
            ////Funciona perfeitamente, porém não retorna o nome do Serviço e do Prestador
            //List<ServicoListItem>? viewModel = new List<ServicoListItem>();

            ////Passando o Id do Prestador
            //var prestador_id = GetIdPrestador();

            //using (var httpClient = new HttpClient())
            //{                
            //    using (var response = await httpClient.GetAsync(urlServicosPrestador + "/" + prestador_id))
            //    {
            //        string responseBody = await response.Content.ReadAsStringAsync();
            //        viewModel = JsonConvert.DeserializeObject<List<ServicoListItem>>(responseBody);
            //    }
            //}
            //return View(viewModel);

            //Criando uma nova Instância
            ServicoViewModel? viewModel = new ServicoViewModel();

            //Preenchendo as Listas            
            viewModel.ServicoList = GetServicosPrestadorList();

            if (viewModel?.ServicoList.Count == 0)
            {
                return RedirectToAction("CadastroServicoPrestador");
            }

            return View(viewModel);            
        }

        [HttpGet]
        public ActionResult CadastroServicoPrestador()
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
                return RedirectToAction("ServicoPrestador");
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View(viewModel); 
        }

        //------------------------------------------------------------------------------------
        //Selects direto no DB - Refatorar a partir daqui:
        //------------------------------------------------------------------------------------

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
                ", Descricao " +
                "FROM Servicos " +
                "Where Servicos.Ativo = @Ativo"
            , con);

            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@Ativo", true);

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
                        Prestador_Id = Convert.ToInt32(PrestadorId),
                        Servico_Id = Convert.ToInt32(idr["Id"]),
                    });
                }
            }
            con.Close();
            return servicoListItem;
        }

        //Lista de Serviços Prestador
        public List<ServicoListItem> GetServicosPrestadorList()
        { 
            //Passando o Id do Prestador
            var prestador_id = GetIdPrestador();

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
                "SELECT ServicosPrestador.Ativo" +
                ", Servicos.Nome" +
                ", Servicos.Descricao" +
                ", ServicosPrestador.Valor " +
                "FROM ServicosPrestador " +
                "INNER JOIN " +
                "Servicos ON ServicosPrestador.ServicoId = Servicos.Id " +
                "Where ServicosPrestador.PrestadorId = @PrestadorId"
                , con);

            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@PrestadorId", prestador_id);

            SqlDataReader idr = cmd.ExecuteReader();
            List<ServicoListItem> servicoListItem = new List<ServicoListItem>();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    servicoListItem.Add(new ServicoListItem
                    {
                        Ativo = Convert.ToBoolean(idr["Ativo"]),
                        Nome = Convert.ToString(idr["Nome"]),                        
                        Descricao = Convert.ToString(idr["Descricao"]),                        
                        Valor = Convert.ToInt32(idr["Valor"]),
                    });
                }
            }
            con.Close();
            return servicoListItem;
        }

    }
}
