using Microsoft.EntityFrameworkCore;
using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Settings;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;
using Pet.WebAPI.Repositories;
using Pet.WebAPI.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Pet.WebAPI.Controllers;

namespace Pet.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment Environment { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //services.Configure<AzureSqlConnection>(options => Configuration.GetSection("AzureSQLConnection").Bind(options)).ToString();

            services.AddDbContext<PetContext>(options =>
                options.UseSqlServer(Configuration.GetSection("AzureSQLConnection")["DefaultConnection"]));

            //TODO extension method for register services
            services.AddScoped<IClientesRepository, ClientesRepository>();
            services.AddTransient<IClientesService, ClientesService>();

            services.AddScoped<IPrestadoresRepository, PrestadoresRepository>();
            services.AddTransient<IPrestadoresService, PrestadoresService>();

            services.AddScoped<IEnderecosPrestadorRepository, EnderecosPrestadorRepository>();
            services.AddTransient<IEnderecosPrestadorService, EnderecosPrestadorService>();

            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddTransient<IUsuariosService, UsuariosService>();

            services.AddScoped<IUsuariosPrestadoresRepository, UsuariosPrestadoresRepository>();
            services.AddTransient<IUsuariosService, UsuariosService>();

            services.AddScoped<IServicosRepository, ServicosRepository>();
            services.AddTransient<IServicosServices, ServicosService>();

            services.AddScoped<IServicosPrestadorRepository, ServicosPrestadorRepository>();
            services.AddTransient<IServicosPrestadorService, ServicosPrestadorService>();

            services.AddScoped<IPetsRepository, PetsRepository>();
            services.AddTransient<IPetsService, PetsService>();


            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
            services.AddTransient<IAgendamentoService, AgendamentoService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, PetContext context)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //context.Database.EnsureDeleted();

            // Create the database if it doesn't exist
            context.Database.EnsureCreated();
        }
    }
}
