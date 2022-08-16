using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Settings;

namespace Pet.Repository.Infrastructure
{
    public class PetContext : DbContext
    {

        private readonly AzureSqlConnection? _connection;
        public PetContext()
        {

        }
        public PetContext(DbContextOptions<PetContext> options, IOptions<AzureSqlConnection> conn) : base(options)
        {
            _connection = conn.Value;
        }

        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<Pets>? Pets { get; set; }
        public DbSet<EnderecoPrestador>? EnderecosPrestadores { get; set; }
        public DbSet<Prestador>? Prestadores { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }

        public DbSet<EnderecoCliente>? EnderecosClientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetSection("AzureSqlConnection:DefaultConnection").Value;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder constructorModel)
        {
            constructorModel.HasDefaultSchema("projetoimpacta");
        
        }

   

    }
}
