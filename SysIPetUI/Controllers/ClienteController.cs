using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        public ActionResult Index()
        {
            //Criando uma nova Instância
            ClienteViewModel? viewModel = new ClienteViewModel();

            //Preenchendo as Listas            
            viewModel.ClienteList = GetClienteList();

            if (viewModel?.ClienteList.Count == 0)
            {
                return RedirectToAction("CadastroCliente");
            }

            return View(viewModel);
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
                //Obtem Id do Usuário logado
                var usuarioId = User.GetIdUsuario();
                cliente.IdUsuario = usuarioId;

                using (var httpClient = new HttpClient())
                {
                    //Serializando os dados da ViewModel ClienteViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Cliente na Tabela do SQL
                    using (var response = await httpClient.PostAsync(url, content))
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
                //Obtem Id do Usuário logado
                var usuarioId = User.GetIdUsuario();
                cliente.IdUsuario = usuarioId;

                using (var httpClient = new HttpClient())
                {
                    //Serializando os dados da ViewModel ClienteViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Cliente na Tabela do SQL
                    using (var response = await httpClient.PostAsync(url, content))
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

        //------------------------------------------------------------------------------------
        //Selects direto no DB - Refatorar a partir daqui:
        //------------------------------------------------------------------------------------

        //Pegando o Id do Cliente
        public int GetIdCliente()
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
            SqlCommand cmd = new SqlCommand("Select Id From Clientes Where IdUsuario = @IdUsuario", con);
            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdUsuario", usuarioId);

            //Executa a consulta e armazena o resultado            
            var result = Convert.ToInt32(cmd.ExecuteScalar());

            //Fecha a conexão
            con.Close();

            return result;
        }

        //Lista de Cliente
        public List<ClienteListItem> GetClienteList()
        {
            //Passando o Id do Cliente
            var clienteId = GetIdCliente();

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
                "SELECT Clientes.Id" +
                ", Clientes.IdUsuario" +
                ", Clientes.NomeCompleto" +
                ", Clientes.CPF" +
                ", Clientes.DataNascimento" +
                ", Clientes.Telefone1" +
                ", Clientes.WhatsApp" +
                ", Clientes.Telefone2" +
                ", EnderecosClientes.ClienteId" +
                ", EnderecosClientes.Logradouro" +
                ", EnderecosClientes.Bairro" +
                ", EnderecosClientes.Complemento" +
                ", EnderecosClientes.Referencia" +
                ", EnderecosClientes.Numero" +
                ", EnderecosClientes.SemNumero" +
                ", EnderecosClientes.Cidade" +
                ", EnderecosClientes.UF" +
                ", EnderecosClientes.CEP " +
                "FROM Clientes " +
                "INNER JOIN " +
                "EnderecosClientes ON Clientes.Id = EnderecosClientes.ClienteId " +
                "Where Clientes.Id = @IdCliente"
                , con);

            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdCliente", clienteId);

            SqlDataReader idr = cmd.ExecuteReader();
            List<ClienteListItem> clienteListItem = new List<ClienteListItem>();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    clienteListItem.Add(new ClienteListItem
                    {
                        Id = Convert.ToInt32(idr["Id"]),
                        IdUsuario = Convert.ToString(idr["IdUsuario"]),
                        NomeCompleto = Convert.ToString(idr["NomeCompleto"]),                        
                        CPF = Convert.ToString(idr["CPF"]),
                        DataNascimento = Convert.ToDateTime(idr["DataNascimento"]),
                        Telefone1 = Convert.ToString(idr["Telefone1"]),
                        WhatsApp = Convert.ToBoolean(idr["WhatsApp"]),
                        Telefone2 = Convert.ToString(idr["Telefone2"]),
                        ClienteId = Convert.ToInt32(idr["ClienteId"]),
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
            return clienteListItem;
        }

        //Se chegamos até aqui algo deu errado e redirecionou para a tela de Erro
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
