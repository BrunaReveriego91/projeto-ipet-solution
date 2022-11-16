using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SysIPetUI.Models;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;

namespace SysIPetUI.Controllers
{
    [Authorize]
    public class AgendamentoController : Controller
    {
        // Pegando as rotas 
        private readonly string url = "https://localhost:44321/api/Agendamento";

        // GET: Agendamento
        public IActionResult Index()
        {
            try
            {
                //Criando uma nova Instância
                AgendamentoViewModel? viewModel = new AgendamentoViewModel();

                //Preenchendo as Listas            
                viewModel.AgendamentoList = GetAgendamentosList();

                if (viewModel?.AgendamentoList.Count == 0)
                {
                    return RedirectToAction("CadastroAgendamento");
                }

                return View(viewModel);                
            }
            catch (Exception)
            {
                return View("Error");
            }
        }               

        // GET: Agendamento/CadastroAgendamento
        public IActionResult CadastroAgendamento()
        {
            //Criando uma nova Instância
            AgendamentoViewModel? viewModel = new AgendamentoViewModel();

            try
            {
                //Preenchendo as Listas            
                viewModel.PrestadorList = GetPrestadorList();

                if (viewModel?.PrestadorList.Count == 0)
                {
                    return View("Error");
                }

                return View(viewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Agendamento/CadastroAgendamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastroAgendamento(AgendamentoViewModel viewModel)
        {
            try 
            {
                //Retorna somente o Prestador Selecionado
                var prestadorSelecionado = viewModel.PrestadorList.Where(x => x.Ativo == true).ToList<PrestadorListItem>();
                viewModel.PrestadorList = prestadorSelecionado;

                //Pega o Id do Prestador Selecionado
                foreach (var item in prestadorSelecionado)
                {
                    viewModel.Id_Prestador = item.Id;
                }

                //Após selecionar o Prestador, retorna a lista de serviços que são prestados pelo prestador escolhido
                if (viewModel.PrestadorList.Count >= 1 && viewModel.Servicos.Count == 0)
                {
                    var Id_Prestador = viewModel.Id_Prestador;
                    //return View(viewModel);
                    return RedirectToAction("CadastroAgendamentoPartial", new { idPrestador = Id_Prestador });
                }

                //Retorna a mensagem de Seleção obrigatória:
                ViewData["Message"] = "Selecione um Prestador para Prosseguir!";

                //Preenchendo as Listas            
                viewModel.PrestadorList = GetPrestadorList();

                return View(viewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        // GET: Agendamento/CadastroAgendamentoPartial        
        public IActionResult CadastroAgendamentoPartial(int idPrestador)
        {
            //Criando uma nova Instância
            AgendamentoViewModel? viewModel = new AgendamentoViewModel();

            //Passando o Id do Prestador
            viewModel.Id_Prestador = idPrestador;

            //Preenchendo as Listas de Serviços e Prestadores
            viewModel.Servicos = GetServicosPrestadorList(idPrestador);
            viewModel.PrestadorList = GetPrestadorIdList(idPrestador);

            return View(viewModel);
        }

        // POST: Agendamento/CadastroAgendamentoPartial
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroAgendamentoPartial(AgendamentoViewModel viewModel)
        {
            //Retorna somente os Serviços Selecionados
            var servicosSelecionado = viewModel.Servicos.Where(x => x.Ativo == true).ToList<ServicoListItem>();
            viewModel.Servicos = servicosSelecionado;

            //Valida se o Serviço foi escolhido
            if (viewModel.Servicos.Count == 0)
            {
                var idPrestador = viewModel.Id_Prestador;

                //Preenchendo as Listas de Serviços e Prestadores
                viewModel.Servicos = GetServicosPrestadorList(idPrestador);
                viewModel.PrestadorList = GetPrestadorIdList(idPrestador);

                //Retorna a mensagem de Seleção obrigatória:
                ViewData["Message"] = "Selecione um Serviço e uma Data para Prosseguir!";

                return View(viewModel);                
            }

            //Passando o Id do Cliente com base no Id do Usuário logado
            viewModel.Id_Cliente = GetIdCliente();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    //Serializando os dados da ViewModel AgendamentoViewModel para JSON e depois convertendo-os em um objeto StringContent:
                    StringContent content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");

                    //Aqui realizamos o PostAsync que Utiliza a Pet.WebAPI para inserir um novo Agendamento na Tabela do SQL
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

        // GET: AgendamentoController/ExcluirAgendamento
        public IActionResult ExcluirAgendamento(int Id)
        {
            AgendamentoViewModel? viewModel = new AgendamentoViewModel();

            viewModel.AgendamentoList = GetAgendamentoIdList(Id);
            
            return View(viewModel);
        }

        // POST: AgendamentoController/ExcluirAgendamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirAgendamento(int Id, IFormCollection form)
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

        //Pegando o Id do Agendamento através do Id do Cliente
        public int GetIdAgendamento()
        {
            //Passando o Id do Cliente com base no Id do Usuário logado
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
            SqlCommand cmd = new SqlCommand("Select Id From Agendamentos Where ClienteId = @IdCliente", con);
            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdCliente", clienteId);

            //Executa a consulta e armazena o resultado            
            var result = Convert.ToInt32(cmd.ExecuteScalar());

            //Fecha a conexão
            con.Close();

            return result;
        }

        //Lista de Prestador
        public List<PrestadorListItem> GetPrestadorList()
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
                "ON Prestadores.Id = EnderecosPrestadores.PrestadorId"
              , con);

            con.Open();            

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

        //Lista de Prestador por Id
        public List<PrestadorListItem> GetPrestadorIdList(int prestador_id)
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
                "WHERE Prestadores.Id = @PrestadorId"
              , con);

            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@PrestadorId", prestador_id);

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
                        Ativo = true,
                    });
                }
            }
            con.Close();
            return prestadorListItem;
        }

        //Lista de Serviços Prestador
        public List<ServicoListItem> GetServicosPrestadorList(int prestador_id)
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
                "SELECT ServicosPrestador.Id AS Id_Servico_Prestador" +
                ", EnderecosPrestadores.Id AS Id_Endereco_Prestador" +
                ", Servicos.Nome" +
                ", Servicos.Descricao" +
                ", ServicosPrestador.Valor " +
                "FROM ServicosPrestador " +
                "INNER JOIN " +
                "Servicos " +
                "ON ServicosPrestador.ServicoId = Servicos.Id " +
                "INNER JOIN " +
                "Prestadores " +
                "ON ServicosPrestador.PrestadorId = Prestadores.Id " +
                "INNER JOIN " +
                "EnderecosPrestadores " +
                "ON Prestadores.Id = EnderecosPrestadores.PrestadorId " +
                "WHERE ServicosPrestador.PrestadorId = @PrestadorId"
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
                        Id_Servico_Prestador = Convert.ToInt32(idr["Id_Servico_Prestador"]),
                        Id_Endereco_Prestador = Convert.ToInt32(idr["Id_Endereco_Prestador"]),
                        Nome = Convert.ToString(idr["Nome"]),
                        Descricao = Convert.ToString(idr["Descricao"]),
                        Valor = Convert.ToInt32(idr["Valor"]),                        
                    });
                }
            }
            con.Close();
            return servicoListItem;
        }

        //Lista de Agendamentos
        public List<AgendamentoListItem> GetAgendamentosList()
        {
            //Passando o Id do Cliente com base no Id do Usuário logado
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
                "SELECT Agendamentos.Id AS AgendamentoId" +
                ", Agendamentos.Data_Agenda AS Data_Agendamento" +
                ", Servicos.Nome AS Servico" +
                ", Servicos.Descricao AS Servico_Descricao" +
                ", Prestadores.NomeCompleto AS Nome_Prestador" +
                ", EnderecosPrestadores.Logradouro AS Rua_Prestador" +
                ", EnderecosPrestadores.Numero AS Numero_Prestador" +
                ", EnderecosPrestadores.Bairro AS Bairro_Prestador" +
                ", EnderecosPrestadores.Cidade AS Cidade_Prestador" +
                ", EnderecosPrestadores.UF AS UF_Prestador" +
                ", Prestadores.Telefone AS Telefone_Prestador" +
                ", Prestadores.Data_Cadastro AS Data_Cadastro_Prestador" +
                ", ServicosPrestador.Valor AS Valor_Servico" +
                ", ServicosAgendamento.Valor_Desconto AS Desconto_Servico" +
                ", ServicosAgendamento.Data_Conclusao" +
                ", ServicosAgendamento.Data_Cancelamento" +
                ", ServicosAgendamento.Mensagem_Profissional_Executante AS Mensagem_Prestador" +
                ", Clientes.NomeCompleto AS Nome_Cliente" +
                ", Clientes.CPF AS CPF_Cliente" +
                ", Clientes.DataNascimento" +
                ", Clientes.Telefone1 AS Telefone_Cliente" +
                ", Clientes.Data_Cadastro AS Data_Cadastro_Cliente" +
                ", Pets.NomeCompleto AS Nome_Pet" +
                ", TipoPet.Descricao AS Tipo_Pet" +
                ", Generos.Descricao AS Genero_Pet" +
                ", TamanhosPet.Descricao AS Tamanho_Pet" +
                ", ServicosAgendamento.Mensagem_Cliente" +
                ", EnderecosClientes.Logradouro AS Rua_Cliente" +
                ", EnderecosClientes.Numero AS Numero_Cliente" +
                ", EnderecosClientes.Bairro AS Bairro_Cliente" +
                ", EnderecosClientes.Cidade AS Cidade_Cliente" +
                ", EnderecosClientes.UF AS UF_Cliente " +
                "FROM Agendamentos " +
                "INNER JOIN " +
                "Prestadores ON Agendamentos.PrestadorId = Prestadores.Id " +
                "INNER JOIN " +
                "ServicosPrestador ON Prestadores.Id = ServicosPrestador.PrestadorId " +
                "INNER JOIN " +
                "ServicosAgendamento ON Agendamentos.Id = ServicosAgendamento.AgendaId AND ServicosPrestador.Id = ServicosAgendamento.ServicoPrestadorId " +
                "INNER JOIN " +
                "Servicos ON ServicosPrestador.ServicoId = Servicos.Id " +
                "INNER JOIN " +
                "EnderecosPrestadores ON Prestadores.Id = EnderecosPrestadores.PrestadorId " +
                "INNER JOIN " +
                "Clientes ON Agendamentos.ClienteId = Clientes.Id " +
                "INNER JOIN " +
                "EnderecosClientes ON Clientes.Id = EnderecosClientes.ClienteId " +
                "INNER JOIN " +
                "Pets ON Clientes.Id = Pets.ClienteId " +
                "INNER JOIN " +
                "TipoPet ON Pets.TipoPet = TipoPet.TipoPetId " +
                "INNER JOIN " +
                "TamanhosPet ON Pets.TamanhoPet = TamanhosPet.TamanhoPetId " +
                "INNER JOIN " +
                "Generos ON Pets.Genero = Generos.GeneroId " +
                "WHERE Agendamentos.ClienteId = @IdCliente"
              , con);

            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdCliente", clienteId);

            SqlDataReader idr = cmd.ExecuteReader();
            List<AgendamentoListItem> agendamentoListItem = new List<AgendamentoListItem>();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    agendamentoListItem.Add(new AgendamentoListItem
                    {
                        AgendamentoId = Convert.ToInt32(idr["AgendamentoId"]),
                        Data_Agendamento = Convert.ToDateTime(idr["Data_Agendamento"]),
                        Servico = Convert.ToString(idr["Servico"]),
                        Servico_Descricao = Convert.ToString(idr["Servico_Descricao"]),
                        Nome_Prestador = Convert.ToString(idr["Nome_Prestador"]),
                        Rua_Prestador = Convert.ToString(idr["Rua_Prestador"]),
                        Numero_Prestador = Convert.ToInt32(idr["Numero_Prestador"]),
                        Bairro_Prestador = Convert.ToString(idr["Bairro_Prestador"]),
                        Cidade_Prestador = Convert.ToString(idr["Cidade_Prestador"]),
                        UF_Prestador = Convert.ToString(idr["UF_Prestador"]),
                        Telefone_Prestador = Convert.ToString(idr["Telefone_Prestador"]),
                        Data_Cadastro_Prestador = Convert.ToDateTime(idr["Data_Cadastro_Prestador"]),
                        Valor_Servico = Convert.ToInt32(idr["Valor_Servico"]),
                        Desconto_Servico = Convert.ToInt32(idr["Desconto_Servico"]),
                        //Data_Conclusao = Convert.ToDateTime(idr["Data_Conclusao"]),
                        //Data_Cancelamento = Convert.ToDateTime(idr["Data_Cancelamento"]),
                        Mensagem_Prestador = Convert.ToString(idr["Mensagem_Prestador"]),
                        Nome_Cliente = Convert.ToString(idr["Nome_Cliente"]),
                        CPF_Cliente = Convert.ToString(idr["CPF_Cliente"]),
                        DataNascimento = Convert.ToDateTime(idr["DataNascimento"]),
                        Telefone_Cliente = Convert.ToString(idr["Telefone_Cliente"]),
                        Data_Cadastro_Cliente = Convert.ToDateTime(idr["Data_Cadastro_Cliente"]),
                        Nome_Pet = Convert.ToString(idr["Nome_Pet"]),
                        Tipo_Pet = Convert.ToString(idr["Tipo_Pet"]),
                        Genero_Pet = Convert.ToString(idr["Genero_Pet"]),
                        Tamanho_Pet = Convert.ToString(idr["Tamanho_Pet"]),
                        Mensagem_Cliente = Convert.ToString(idr["Mensagem_Cliente"]),
                        Rua_Cliente = Convert.ToString(idr["Rua_Cliente"]),
                        Numero_Cliente = Convert.ToInt32(idr["Numero_Cliente"]),
                        Bairro_Cliente = Convert.ToString(idr["Bairro_Cliente"]),
                        Cidade_Cliente = Convert.ToString(idr["Cidade_Cliente"]),
                        UF_Cliente = Convert.ToString(idr["UF_Cliente"]),
                    });
                }
            }
            con.Close();
            return agendamentoListItem;
        }

        //Lista de Agendamentos by Id
        public List<AgendamentoListItem> GetAgendamentoIdList(int Id)
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
                "SELECT Agendamentos.Id AS AgendamentoId" +
                ", Agendamentos.Data_Agenda AS Data_Agendamento" +
                ", Servicos.Nome AS Servico" +
                ", Servicos.Descricao AS Servico_Descricao" +
                ", Prestadores.NomeCompleto AS Nome_Prestador" +
                ", EnderecosPrestadores.Logradouro AS Rua_Prestador" +
                ", EnderecosPrestadores.Numero AS Numero_Prestador" +
                ", EnderecosPrestadores.Bairro AS Bairro_Prestador" +
                ", EnderecosPrestadores.Cidade AS Cidade_Prestador" +
                ", EnderecosPrestadores.UF AS UF_Prestador" +
                ", Prestadores.Telefone AS Telefone_Prestador" +
                ", Prestadores.Data_Cadastro AS Data_Cadastro_Prestador" +
                ", ServicosPrestador.Valor AS Valor_Servico" +
                ", ServicosAgendamento.Valor_Desconto AS Desconto_Servico" +
                ", ServicosAgendamento.Data_Conclusao" +
                ", ServicosAgendamento.Data_Cancelamento" +
                ", ServicosAgendamento.Mensagem_Profissional_Executante AS Mensagem_Prestador" +
                ", Clientes.NomeCompleto AS Nome_Cliente" +
                ", Clientes.CPF AS CPF_Cliente" +
                ", Clientes.DataNascimento" +
                ", Clientes.Telefone1 AS Telefone_Cliente" +
                ", Clientes.Data_Cadastro AS Data_Cadastro_Cliente" +
                ", Pets.NomeCompleto AS Nome_Pet" +
                ", TipoPet.Descricao AS Tipo_Pet" +
                ", Generos.Descricao AS Genero_Pet" +
                ", TamanhosPet.Descricao AS Tamanho_Pet" +
                ", ServicosAgendamento.Mensagem_Cliente" +
                ", EnderecosClientes.Logradouro AS Rua_Cliente" +
                ", EnderecosClientes.Numero AS Numero_Cliente" +
                ", EnderecosClientes.Bairro AS Bairro_Cliente" +
                ", EnderecosClientes.Cidade AS Cidade_Cliente" +
                ", EnderecosClientes.UF AS UF_Cliente " +
                "FROM Agendamentos " +
                "INNER JOIN " +
                "Prestadores ON Agendamentos.PrestadorId = Prestadores.Id " +
                "INNER JOIN " +
                "ServicosPrestador ON Prestadores.Id = ServicosPrestador.PrestadorId " +
                "INNER JOIN " +
                "ServicosAgendamento ON Agendamentos.Id = ServicosAgendamento.AgendaId AND ServicosPrestador.Id = ServicosAgendamento.ServicoPrestadorId " +
                "INNER JOIN " +
                "Servicos ON ServicosPrestador.ServicoId = Servicos.Id " +
                "INNER JOIN " +
                "EnderecosPrestadores ON Prestadores.Id = EnderecosPrestadores.PrestadorId " +
                "INNER JOIN " +
                "Clientes ON Agendamentos.ClienteId = Clientes.Id " +
                "INNER JOIN " +
                "EnderecosClientes ON Clientes.Id = EnderecosClientes.ClienteId " +
                "INNER JOIN " +
                "Pets ON Clientes.Id = Pets.ClienteId " +
                "INNER JOIN " +
                "TipoPet ON Pets.TipoPet = TipoPet.TipoPetId " +
                "INNER JOIN " +
                "TamanhosPet ON Pets.TamanhoPet = TamanhosPet.TamanhoPetId " +
                "INNER JOIN " +
                "Generos ON Pets.Genero = Generos.GeneroId " +
                "WHERE Agendamentos.Id = @IdAgendamento"
              , con);

            con.Open();

            //Parâmetros do Where
            cmd.Parameters.AddWithValue("@IdAgendamento", Id);

            SqlDataReader idr = cmd.ExecuteReader();
            List<AgendamentoListItem> agendamentoListItem = new List<AgendamentoListItem>();

            if (idr.HasRows)
            {
                while (idr.Read())
                {
                    agendamentoListItem.Add(new AgendamentoListItem
                    {
                        AgendamentoId = Convert.ToInt32(idr["AgendamentoId"]),
                        Data_Agendamento = Convert.ToDateTime(idr["Data_Agendamento"]),
                        Servico = Convert.ToString(idr["Servico"]),
                        Servico_Descricao = Convert.ToString(idr["Servico_Descricao"]),
                        Nome_Prestador = Convert.ToString(idr["Nome_Prestador"]),
                        Rua_Prestador = Convert.ToString(idr["Rua_Prestador"]),
                        Numero_Prestador = Convert.ToInt32(idr["Numero_Prestador"]),
                        Bairro_Prestador = Convert.ToString(idr["Bairro_Prestador"]),
                        Cidade_Prestador = Convert.ToString(idr["Cidade_Prestador"]),
                        UF_Prestador = Convert.ToString(idr["UF_Prestador"]),
                        Telefone_Prestador = Convert.ToString(idr["Telefone_Prestador"]),
                        Data_Cadastro_Prestador = Convert.ToDateTime(idr["Data_Cadastro_Prestador"]),
                        Valor_Servico = Convert.ToInt32(idr["Valor_Servico"]),
                        Desconto_Servico = Convert.ToInt32(idr["Desconto_Servico"]),
                        //Data_Conclusao = Convert.ToDateTime(idr["Data_Conclusao"]),
                        //Data_Cancelamento = Convert.ToDateTime(idr["Data_Cancelamento"]),
                        Mensagem_Prestador = Convert.ToString(idr["Mensagem_Prestador"]),
                        Nome_Cliente = Convert.ToString(idr["Nome_Cliente"]),
                        CPF_Cliente = Convert.ToString(idr["CPF_Cliente"]),
                        DataNascimento = Convert.ToDateTime(idr["DataNascimento"]),
                        Telefone_Cliente = Convert.ToString(idr["Telefone_Cliente"]),
                        Data_Cadastro_Cliente = Convert.ToDateTime(idr["Data_Cadastro_Cliente"]),
                        Nome_Pet = Convert.ToString(idr["Nome_Pet"]),
                        Tipo_Pet = Convert.ToString(idr["Tipo_Pet"]),
                        Genero_Pet = Convert.ToString(idr["Genero_Pet"]),
                        Tamanho_Pet = Convert.ToString(idr["Tamanho_Pet"]),
                        Mensagem_Cliente = Convert.ToString(idr["Mensagem_Cliente"]),
                        Rua_Cliente = Convert.ToString(idr["Rua_Cliente"]),
                        Numero_Cliente = Convert.ToInt32(idr["Numero_Cliente"]),
                        Bairro_Cliente = Convert.ToString(idr["Bairro_Cliente"]),
                        Cidade_Cliente = Convert.ToString(idr["Cidade_Cliente"]),
                        UF_Cliente = Convert.ToString(idr["UF_Cliente"]),
                    });
                }
            }
            con.Close();
            return agendamentoListItem;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
