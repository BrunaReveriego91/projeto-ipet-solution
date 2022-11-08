using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SysIPetUI.Models;
using SysIPetUI.Services;
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
        public IActionResult Index()
        {
            //Criando uma nova Instância
            PetsListViewModel? viewModel = new PetsListViewModel();

            //Preenchendo as Listas            
            viewModel.PetsList = GetPetsList();

            if (viewModel?.PetsList.Count == 0)
            {
                return RedirectToAction("CadastroPet");
            }

            return View(viewModel);
        }              

        // GET: PetController/Create
        public ActionResult CreatePet()
        {
            //Instancia a PetsListViewModel e Preenche as Listas
            PetsListViewModel viewModel = new PetsListViewModel
            {
                TipoPetList = GetTipoPetList(),
                TamanhoPetList = GetTamanhoPetList(),
                GeneroPetList = GetGeneroPetList()
            };                       

            return View(viewModel);
        }

        // POST: PetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePet(PetsListViewModel? viewModel)
        {
            try
            {
                //Passando o Id do Cliente
                viewModel.PetsViewModelInstancia.ClienteId = GetIdCliente();

                //Preenchendo as Listas
                viewModel.TipoPetList = GetTipoPetList();
                viewModel.TamanhoPetList = GetTamanhoPetList();
                viewModel.GeneroPetList = GetGeneroPetList();

                using (var httpClient = new HttpClient())
                {
                    //Serializando os dados da ViewModel PetsListViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(viewModel?.PetsViewModelInstancia), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Pet na Tabela do SQL
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

        // GET: PetController/CadastroPet
        public ActionResult CadastroPet()
        {
            //Instancia a PetsListViewModel e Preenche as Listas
            PetsListViewModel viewModel = new PetsListViewModel
            {
                TipoPetList = GetTipoPetList(),
                TamanhoPetList = GetTamanhoPetList(),
                GeneroPetList = GetGeneroPetList()
            };

            return View(viewModel);
        }

        // POST: PetController/CadastroPet ok
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroPet(PetsListViewModel? viewModel)
        {
            try
            {
                //Passando o Id do Cliente
                viewModel.PetsViewModelInstancia.ClienteId = GetIdCliente();

                //Preenchendo as Listas
                viewModel.TipoPetList = GetTipoPetList();
                viewModel.TamanhoPetList = GetTamanhoPetList();
                viewModel.GeneroPetList = GetGeneroPetList();                

                using (var httpClient = new HttpClient())
                {
                    //Serializando os dados da ViewModel PetsListViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(viewModel?.PetsViewModelInstancia), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Pet na Tabela do SQL
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

        // GET: PetController/Edit/ ok
        [HttpGet]
        public async Task<IActionResult> EditPet(int Id)
        {
            //Criando uma nova Instância
            PetsListViewModel? viewModel = new PetsListViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Pet que será Editado pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Pet que será Editado.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    //Lê os dados e salva no responseBody
                    string responseBody = await response.Content.ReadAsStringAsync();

                    //Preenche a PetsViewModelInstancia com os Dados do DB
                    viewModel.PetsViewModelInstancia = JsonConvert.DeserializeObject<PetsViewModel>(responseBody);
                }
            }

            //Preenchendo as Listas
            viewModel.TipoPetList = GetTipoPetList();
            viewModel.TamanhoPetList = GetTamanhoPetList();
            viewModel.GeneroPetList = GetGeneroPetList();

            return View(viewModel);
        }

        // POST: PetController/Edit/ ok
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPet(PetsListViewModel? viewModel)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //Aqui realizamos o PutAsync que Utiliza a Pet.WebAPI para Editar o Pet na Tabela do SQL usando o Id
                    using (var response = await httpClient.PutAsJsonAsync($"{url}/{viewModel?.PetsViewModelInstancia?.Id}", viewModel?.PetsViewModelInstancia))
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

        // GET: PetController/ExcluirPet/ ok
        public async Task<IActionResult> ExcluirPet(int Id)
        {
            //Criando uma nova Instância
            PetsListViewModel? viewModel = new PetsListViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Pet que será Excluído pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Pet que será Editado.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    //Lê os dados e salva no responseBody
                    string responseBody = await response.Content.ReadAsStringAsync();

                    //Preenche a PetsViewModelInstancia com os Dados do DB
                    viewModel.PetsViewModelInstancia = JsonConvert.DeserializeObject<PetsViewModel>(responseBody);
                }
            }

            //Preenchendo as Listas            
            viewModel.PetsList = GetPetsList();

            return View(viewModel);
        }

        // POST: PetController/ExcluirPet/ ok
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

        // GET: PetController/DetailsPet/ ok
        public async Task<IActionResult> DetailsPet(int Id)
        {
            //Criando uma nova Instância
            PetsListViewModel? viewModel = new PetsListViewModel();

            using (var httpClient = new HttpClient())
            {
                //Aqui realizamos o GetAsync que Utiliza a Pet.WebAPI para Retornar o Pet que será Excluído pelo Id na Tabela do SQL
                //Dessa forma exibimos no Modal os Detalhes do Pet que será Editado.
                using (var response = await httpClient.GetAsync(url + "/" + Id))
                {
                    //Lê os dados e salva no responseBody
                    string responseBody = await response.Content.ReadAsStringAsync();

                    //Preenche a PetsViewModelInstancia com os Dados do DB
                    viewModel.PetsViewModelInstancia = JsonConvert.DeserializeObject<PetsViewModel>(responseBody);
                }
            }

            //Preenchendo as Listas            
            viewModel.PetsList = GetPetsList();

            return View(viewModel);
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

        //DropdownList TipoPet
        public List<TipoPet> GetTipoPetList()
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
            SqlCommand cmd = new SqlCommand("Select TipoPetId, Descricao From TipoPet", con);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();
            List<TipoPet> tipoPet = new List<TipoPet>();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    tipoPet.Add(new TipoPet
                    {
                        TipoPetId = Convert.ToInt32(idr["TipoPetId"]),
                        Descricao = Convert.ToString(idr["Descricao"]),
                    });
                }
            }
            con.Close();
            return tipoPet;
        }

        //DropdownList Tamanho Pet
        public List<TamanhoPet> GetTamanhoPetList()
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
            SqlCommand cmd = new SqlCommand("Select TamanhoPetId, Descricao From TamanhosPet", con);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();
            List<TamanhoPet> tamanhoPet = new List<TamanhoPet>();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    tamanhoPet.Add(new TamanhoPet
                    {
                        TamanhoPetId = Convert.ToInt32(idr["TamanhoPetId"]),
                        Descricao = Convert.ToString(idr["Descricao"]),
                    });
                }
            }
            con.Close();
            return tamanhoPet;
        }

        //DropdownList Genero Pet
        public List<GeneroPet> GetGeneroPetList()
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
            SqlCommand cmd = new SqlCommand("Select GeneroId, Descricao From Generos", con);
            con.Open();

            SqlDataReader idr = cmd.ExecuteReader();
            List<GeneroPet> generoPet = new List<GeneroPet>();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    generoPet.Add(new GeneroPet
                    {
                        GeneroPetId = Convert.ToInt32(idr["GeneroId"]),
                        Descricao = Convert.ToString(idr["Descricao"]),
                    });
                }
            }
            con.Close();
            return generoPet;
        }

        //Lista de Pets
        public List<PetsListItem> GetPetsList()
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
                "SELECT Pets.Id" +
                ", Pets.ClienteId" +
                ", Pets.NomeCompleto" +
                ", Pets.TipoPet" +
                ", TipoPet.Descricao AS TipoPetNome" +
                ", Pets.TamanhoPet" +
                ", TamanhosPet.Descricao AS TamanhoPetNome" +
                ", Pets.Genero" +
                ", Generos.Descricao AS GeneroPetNome" +
                ", Pets.Peso" +
                ", Pets.Cor" +
                ", Pets.DataNascimento" +
                ", Pets.Raca" +
                ", Pets.Data_Cadastro " +
                "FROM Pets " +
                "INNER JOIN " +
                "TipoPet " +
                "ON Pets.TipoPet = TipoPet.TipoPetId " +
                "INNER JOIN " +
                "TamanhosPet " +
                "ON Pets.TamanhoPet = TamanhosPet.TamanhoPetId " +
                "INNER JOIN " +
                "Generos " +
                "ON Pets.Genero = Generos.GeneroId " +
                "Where Pets.ClienteId = @IdCliente"
                , con);

            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdCliente", clienteId);

            SqlDataReader idr = cmd.ExecuteReader();
            List<PetsListItem> petsListItem = new List<PetsListItem>();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    petsListItem.Add(new PetsListItem
                    {
                        Id = Convert.ToInt32(idr["Id"]),
                        ClienteId = Convert.ToInt32(idr["ClienteId"]),
                        NomeCompleto = Convert.ToString(idr["NomeCompleto"]),
                        TipoPetId = Convert.ToInt32(idr["TipoPet"]),
                        TipoPetNome = Convert.ToString(idr["TipoPetNome"]),
                        TamanhoPetId = Convert.ToInt32(idr["TamanhoPet"]),
                        TamanhoPetNome = Convert.ToString(idr["TamanhoPetNome"]),
                        GeneroPetId = Convert.ToInt32(idr["Genero"]),
                        GeneroPetNome = Convert.ToString(idr["GeneroPetNome"]),
                        Peso = Convert.ToDouble(idr["Peso"]),
                        Cor = Convert.ToString(idr["Cor"]),
                        DataNascimento = Convert.ToDateTime(idr["DataNascimento"]),
                        Raca = Convert.ToString(idr["Raca"]),
                    });
                }
            }
            con.Close();
            return petsListItem;
        }

        //Se chegamos até aqui algo deu errado e redirecionou para a tela de Erro
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
