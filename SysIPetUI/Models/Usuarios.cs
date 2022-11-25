using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace SysIPetUI.Models
{
    public static class Usuarios
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tipoUsuario = GetTipoUsuario(userId);

            return tipoUsuario;
        }

        public static string GetIdUsuario(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;            

            return userId;
        }

        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }
            return claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
        }

        [HttpGet("{UserId}")]
        public static string GetTipoUsuario(string? userId)
        {
            //Encontra e faz a leitura do arquivo appsettings.json:
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            //Conexão com o LocalDB:
            var stringConexao = configuration.GetConnectionString("DefaultConnection");
            SqlConnection sqlconn = new SqlConnection(stringConexao);

            //Script SQL para realizar o Insert no DB:
            string sqlquery = "Select TipoUsuario from [dbo].[AspNetTipoUsuario] Where UserId = @userId";

            //Cria um novo Comando SQL
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);

            //Abre a Conexão:
            sqlconn.Open();

            //Instrução de Seleção dos dados
            sqlcomm.Parameters.AddWithValue("@UserId", userId);

            //Converte o Result em string para retornar True ou False
            var result = Convert.ToString(sqlcomm.ExecuteScalar());

            //Fecha a Conexão:
            sqlconn.Close();

            return result;
        }

        public static string GetPrestadorNome(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var nomePrestador = GetNomePrestador(userId);

            return nomePrestador;
        }

        [HttpGet("{UserId}")]
        public static string GetNomePrestador(string? userId)
        {
            //Encontra e faz a leitura do arquivo appsettings.json:
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            //Conexão com o LocalDB:
            var stringConexao = configuration.GetConnectionString("DefaultConnection");
            SqlConnection sqlconn = new SqlConnection(stringConexao);

            //Script SQL para realizar o Insert no DB:
            string sqlquery = "Select NomeCompleto from [dbo].[Prestadores] Where Id_Prestador = @userId";

            //Cria um novo Comando SQL
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);

            //Abre a Conexão:
            sqlconn.Open();

            //Instrução de Seleção dos dados
            sqlcomm.Parameters.AddWithValue("@UserId", userId);

            //Converte o Result em string para retornar o nome do Prestador
            var result = Convert.ToString(sqlcomm.ExecuteScalar());

            //Fecha a Conexão:
            sqlconn.Close();

            return result;
        }

        public static string GetClienteNome(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var nomeCliente = GetNomeCliente(userId);

            return nomeCliente;
        }

        [HttpGet("{UserId}")]
        public static string GetNomeCliente(string? userId)
        {
            //Encontra e faz a leitura do arquivo appsettings.json:
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            //Conexão com o LocalDB:
            var stringConexao = configuration.GetConnectionString("DefaultConnection");
            SqlConnection sqlconn = new SqlConnection(stringConexao);

            //Script SQL para realizar o Insert no DB:
            string sqlquery = "Select NomeCompleto from [dbo].[Clientes] Where IdUsuario = @userId";

            //Cria um novo Comando SQL
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);

            //Abre a Conexão:
            sqlconn.Open();

            //Instrução de Seleção dos dados
            sqlcomm.Parameters.AddWithValue("@UserId", userId);

            //Converte o Result em string para retornar o nome do Prestador
            var result = Convert.ToString(sqlcomm.ExecuteScalar());

            //Fecha a Conexão:
            sqlconn.Close();

            return result;
        }

    }
}
