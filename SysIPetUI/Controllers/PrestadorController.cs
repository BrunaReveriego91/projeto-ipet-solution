using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Policy;
using System.Text;

namespace SysIPetUI.Controllers
{
    [Authorize]
    public class PrestadorController : Controller
    {
        // Pegando o endereço com HttpClient        
        private readonly string urlPrestadores = "https://localhost:44321/api/Prestadores";
        private readonly string urlServicos = "https://localhost:44321/api/Servicos";
        private readonly string urlServicosPrestador = "https://localhost:44321/api/ServicosPrestador";
        private readonly string urlEnderecoPrestador = "https://localhost:44321/api/EnderecoPrestador";        

        // GET: PrestadorController
        public async Task<IActionResult> Index()
        {
            //Criando uma nova Instância
            PrestadorViewModel? viewModel = new PrestadorViewModel();            

            //Preenchendo as Listas            
            viewModel.PrestadorList = GetPrestadorList();

            if (viewModel?.PrestadorList.Count == 0)
            {
                return RedirectToAction("CadastroPrestador");
            }

            return View(viewModel);
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
                //Obtem Id do Usuário logado
                var prestadorId = User.GetIdUsuario();
                prestador.Prestador.Id_Prestador = prestadorId;

                using (var httpClient = new HttpClient())
                {
                    //Cadastra o Prestador
                    //Serializando os dados da ViewModel PrestadorViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(prestador.Prestador), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Prestador na Tabela do SQL
                    using (var response = await httpClient.PostAsync(urlPrestadores, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                    }

                    //Cadastra o Endereço do Prestador
                    //Passando o Id do Prestador
                    prestador.Endereco.PrestadorId = GetIdPrestador();

                    //Serializando os dados da ViewModel PrestadorViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent contentEndereco = new StringContent(JsonConvert.SerializeObject(prestador.Endereco), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir o Endereço do Prestador na Tabela do SQL
                    using (var response = await httpClient.PostAsync(urlEnderecoPrestador, contentEndereco))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // GET: PrestadorController/CadastroPrestador
        public ActionResult CadastroPrestador()
        {            
            return View();
        }

        // POST: PrestadorController/CadastroPrestador
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroPrestador(PrestadorViewModel viewModel)
        {
            try
            {
                //Obtem Id do Usuário logado
                var prestadorId = User.GetIdUsuario();
                viewModel.Prestador.Id_Prestador = prestadorId;

                using (var httpClient = new HttpClient())
                {
                    //Cadastra o Prestador
                    //Serializando os dados da ViewModel PrestadorViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(viewModel.Prestador), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Prestador na Tabela do SQL
                    using (var response = await httpClient.PostAsync(urlPrestadores, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                    }

                    //Cadastra o Endereço do Prestador
                    //Passando o Id do Prestador
                    viewModel.Endereco.PrestadorId = GetIdPrestador();

                    //Serializando os dados da ViewModel PrestadorViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent contentEndereco = new StringContent(JsonConvert.SerializeObject(viewModel.Endereco), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir o Endereço do Prestador na Tabela do SQL
                    using (var response = await httpClient.PostAsync(urlEnderecoPrestador, contentEndereco))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
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
        public IActionResult EditPrestador(int Id)
        {
            PrestadorViewModel? viewModel = new PrestadorViewModel();
            viewModel = GetPrestadorViewModel();
            return View(viewModel);

            //PrestadorViewModel? viewModel = new PrestadorViewModel();
            //viewModel.PrestadorList = GetPrestadorList();
            //return View(viewModel);

            //PrestadorViewModel? viewModel = new PrestadorViewModel();

            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync(urlPrestadores + "/" + Id))
            //    {
            //        string responseBody = await response.Content.ReadAsStringAsync();
            //        viewModel = JsonConvert.DeserializeObject<PrestadorViewModel>(responseBody);
            //    }
            //}

            //return View(viewModel);
        }

        // POST: PrestadorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPrestador(PrestadorViewModel viewModel)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //Aqui realizamos o PutAsync que Utiliza a Pet.WebAPI para Editar o Prestador na Tabela do SQL usando o Id
                    using (var response = await httpClient.PutAsJsonAsync($"{urlPrestadores}/{viewModel.Id}", viewModel))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();                        
                    }

                    //Cadastra o Endereço do Prestador
                    //Passando o Id do Prestador
                    viewModel.IdEnderecoPrestador = GetIdEnderecoPrestador();                    

                    //Aqui realizamos o PutAsync que Utiliza a Pet.WebAPI para Editar o Endereço do Prestador na Tabela do SQL usando o Id
                    using (var response = await httpClient.PutAsJsonAsync($"{urlEnderecoPrestador}/{viewModel.IdEnderecoPrestador}", viewModel))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
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
                using (var response = await httpClient.GetAsync(urlPrestadores + "/" + Id))
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
                var IdEnderecoPrestador = GetIdEnderecoPrestador();

                //Aqui realizamos o DeletAsync que Utiliza a Pet.WebAPI para Deletar o Prestador na Tabela do SQL usando o Id
                //Deleta Endereço Prestador
                using (var responseEnderecoPrestador = await httpClient.DeleteAsync(urlEnderecoPrestador + "/" + IdEnderecoPrestador))
                {
                    string responseBodyEnderecoPrestador = await responseEnderecoPrestador.Content.ReadAsStringAsync();
                }

                //Deleta Prestador
                using (var response = await httpClient.DeleteAsync(urlPrestadores + "/" + Id))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
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

        //Pegando o Id do Endereço Prestador
        public int GetIdEnderecoPrestador()
        {
            //Obtem Id do Prestador
            var prestadorId = GetIdPrestador();

            //Encontra e faz a leitura do arquivo appsettings.json:
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            //Conexão com o LocalDB
            var stringConexao = configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(stringConexao);

            //Select na Tabela
            SqlCommand cmd = new SqlCommand("Select Id From EnderecosPrestadores Where PrestadorId = @IdPrestador", con);
            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdPrestador", prestadorId);

            //Executa a consulta e armazena o resultado            
            var result = Convert.ToInt32(cmd.ExecuteScalar());

            //Fecha a conexão
            con.Close();

            return result;
        }

        //Lista de Prestador
        public List<PrestadorListItem> GetPrestadorList()
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
            SqlCommand cmd = new SqlCommand
            (
                "SELECT Prestadores.Id" +
                ", Prestadores.NomeCompleto" +
                ", Prestadores.CPF_CNPJ" +
                ", Prestadores.Telefone" +
                ", Prestadores.WhatsApp" +
                ", Prestadores.Id_Prestador" +
                ", EnderecosPrestadores.PrestadorId" +
                ", EnderecosPrestadores.Logradouro" +
                ", EnderecosPrestadores.Bairro" +
                ", EnderecosPrestadores.Complemento" +
                ", EnderecosPrestadores.Referencia" +
                ", EnderecosPrestadores.Numero" +
                ", EnderecosPrestadores.SemNumero" +
                ", EnderecosPrestadores.Cidade" +
                ", EnderecosPrestadores.UF" +
                ", EnderecosPrestadores.CEP " +
                "FROM Prestadores " +
                "INNER JOIN " +
                "EnderecosPrestadores " +
                "ON Prestadores.Id = EnderecosPrestadores.PrestadorId " +
                "Where Prestadores.Id_Prestador = @IdUsuario"
              , con);

            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdUsuario", usuarioId);

            SqlDataReader idr = cmd.ExecuteReader();
            List<PrestadorListItem> prestadorListItem = new List<PrestadorListItem>();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    prestadorListItem.Add(new PrestadorListItem
                    {
                        Id = Convert.ToInt32(idr["Id"]),
                        NomeCompleto = Convert.ToString(idr["NomeCompleto"]),
                        CPF_CNPJ = Convert.ToString(idr["CPF_CNPJ"]),
                        Telefone = Convert.ToString(idr["Telefone"]),
                        WhatsApp = Convert.ToBoolean(idr["WhatsApp"]),
                        Id_Prestador = Convert.ToString(idr["Id_Prestador"]),
                        PrestadorId = Convert.ToInt32(idr["PrestadorId"]),
                        Logradouro = Convert.ToString(idr["Logradouro"]),
                        Bairro = Convert.ToString(idr["Bairro"]),
                        Complemento = Convert.ToString(idr["Complemento"]),
                        Referencia = Convert.ToString(idr["Referencia"]),
                        Numero = Convert.ToInt32(idr["Numero"]),
                        SemNumero = Convert.ToBoolean(idr["SemNumero"]),
                        Cidade = Convert.ToString(idr["Cidade"]),
                        UF = Convert.ToString(idr["UF"]),
                        CEP = Convert.ToString(idr["CEP"]),
                    });
                }
            }
            con.Close();
            return prestadorListItem;
        }

        //Get PrestadorViewModel
        public PrestadorViewModel GetPrestadorViewModel()
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
            SqlCommand cmd = new SqlCommand
            (
                "SELECT Prestadores.Id" +
                ", Prestadores.NomeCompleto" +
                ", Prestadores.CPF_CNPJ" +
                ", Prestadores.Telefone" +
                ", Prestadores.WhatsApp" +
                ", Prestadores.Id_Prestador" +
                ", EnderecosPrestadores.PrestadorId" +
                ", EnderecosPrestadores.Logradouro" +
                ", EnderecosPrestadores.Bairro" +
                ", EnderecosPrestadores.Complemento" +
                ", EnderecosPrestadores.Referencia" +
                ", EnderecosPrestadores.Numero" +
                ", EnderecosPrestadores.SemNumero" +
                ", EnderecosPrestadores.Cidade" +
                ", EnderecosPrestadores.UF" +
                ", EnderecosPrestadores.CEP " +
                "FROM Prestadores " +
                "INNER JOIN " +
                "EnderecosPrestadores " +
                "ON Prestadores.Id = EnderecosPrestadores.PrestadorId " +
                "Where Prestadores.Id_Prestador = @IdUsuario"
              , con);

            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdUsuario", usuarioId);

            SqlDataReader idr = cmd.ExecuteReader();
            PrestadorViewModel prestadorViewModel = new PrestadorViewModel();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    prestadorViewModel = new PrestadorViewModel
                    {
                        Id = Convert.ToInt32(idr["Id"]),
                        NomeCompleto = Convert.ToString(idr["NomeCompleto"]),
                        CPF_CNPJ = Convert.ToString(idr["CPF_CNPJ"]),
                        Telefone = Convert.ToString(idr["Telefone"]),
                        WhatsApp = Convert.ToBoolean(idr["WhatsApp"]),
                        Id_Prestador = Convert.ToString(idr["Id_Prestador"]),
                        PrestadorId = Convert.ToInt32(idr["PrestadorId"]),
                        Logradouro = Convert.ToString(idr["Logradouro"]),
                        Bairro = Convert.ToString(idr["Bairro"]),
                        Complemento = Convert.ToString(idr["Complemento"]),
                        Referencia = Convert.ToString(idr["Referencia"]),
                        Numero = Convert.ToInt32(idr["Numero"]),
                        SemNumero = Convert.ToBoolean(idr["SemNumero"]),
                        Cidade = Convert.ToString(idr["Cidade"]),
                        UF = Convert.ToString(idr["UF"]),
                        CEP = Convert.ToString(idr["CEP"]),
                    };
                }
            }
            con.Close();
            return prestadorViewModel;
        }

        //Se chegamos até aqui algo deu errado e redirecionou para a tela de Erro
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
