using Microsoft.EntityFrameworkCore;
using Pet.Repository.Infrastructure;
using Pet.WebAPI.Domain.Settings;
using Pet.WebAPI.Interfaces.Repositories;
using Pet.WebAPI.Interfaces.Services;
using Pet.WebAPI.Repositories;
using Pet.WebAPI.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.Configure<AzureSqlConnection>(options => Configuration.GetSection("AzureSQLConnection").Bind(options)).ToString();
            //services.AddDbContext<PetContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //TODO extension method for register services
            services.AddScoped<IClientPetRepository, ClientPetRepository>();
            services.AddTransient<IClientPetService, ClientPetService>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseAuthorization();


        }



    }
}
