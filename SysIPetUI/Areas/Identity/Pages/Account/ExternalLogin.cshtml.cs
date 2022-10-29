// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SysIPetUI.Models;
using Microsoft.Data.SqlClient;

namespace SysIPetUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;

        public ExternalLoginModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
            _emailSender = emailSender;
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
        public string ProviderDisplayName { get; set; }

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
        }
        
        public IActionResult OnGet() => RedirectToPage("./Login");

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);

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
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the external login page in /Areas/Identity/Pages/Account/ExternalLogin.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
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
