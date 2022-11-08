// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SysIPetUI.Models;
using Microsoft.Data.SqlClient;

namespace SysIPetUI.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    ////Verifica se ainda não foram cadastrados
                    ////Cliente - Criando uma nova Instância
                    //ClienteViewModel clienteViewModel = new ClienteViewModel();

                    ////Preenchendo as Listas            
                    //clienteViewModel.ClienteList = GetClienteList();

                    //if (clienteViewModel?.ClienteList.Count == 0)
                    //{
                    //    return RedirectToAction("CadastroCliente");
                    //}

                    ////Pet - Criando uma nova Instância
                    //PetsListViewModel petViewModel = new PetsListViewModel();

                    ////Preenchendo as Listas            
                    //petViewModel.PetsList = GetPetsList();

                    //if (petViewModel?.PetsList.Count == 0)
                    //{
                    //    return RedirectToAction("CadastroPet");
                    //}

                    ////Prestador - Criando uma nova Instância
                    //PrestadorViewModel prestadorViewModel = new PrestadorViewModel();

                    ////Preenchendo as Listas            
                    //prestadorViewModel.PrestadorList = GetPrestadorList();

                    //if (prestadorViewModel?.PrestadorList.Count == 0)
                    //{
                    //    return RedirectToAction("CadastroPrestador");
                    //}

                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
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

    }

}
