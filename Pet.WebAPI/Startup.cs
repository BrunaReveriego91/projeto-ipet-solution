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

            services.Configure<AzureSqlConnection>(options => Configuration.GetSection("AzureSQLConnection").Bind(options)).ToString();
            
            services.AddDbContext<PetContext>(options =>
                options.UseSqlServer(Configuration.GetSection("AzureSQLConnection")["DefaultConnection"]));

            //TODO extension method for register services
            services.AddScoped<IClienteRepository, ClientesRepository>();
            services.AddTransient<IClienteService, ClienteService>();

            //services.AddTransient<IPrestadoresController, PrestadoresController>();
            services.AddScoped<IPrestadoresRepository, PrestadoresRepository>();
            services.AddTransient<IPrestadoresService, PrestadoresService>();

            //services.AddScoped<IEnderecoPrestadorController, EnderecoPrestadorController>();
            services.AddScoped<IEnderecosPrestadorRepository, EnderecosPrestadorRepository>();
            services.AddTransient<IEnderecosPrestadorService, EnderecosPrestadorService>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
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
        }
    }
}
