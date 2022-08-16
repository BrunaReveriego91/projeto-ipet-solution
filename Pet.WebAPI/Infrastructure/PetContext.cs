using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Settings;

namespace Pet.Repository.Infrastructure
{
    public class PetContext : DbContext
    {
        private readonly AzureSqlConnection _connection;
        public PetContext()
        {

        }
        public PetContext(DbContextOptions<PetContext> options, IOptions<AzureSqlConnection> conn) : base(options)
        {
            _connection = conn.Value;
        }

        //public DbSet<Client> Clients { get; set; }
        public DbSet<ClientPet> Pets { get; set; }
        public DbSet<EnderecoPrestador> EnderecosPrestadores { get; set; }
        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

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
            ConfigureClientPet(constructorModel);
        }

        private void ConfigureClientPet(ModelBuilder constructorModel)
        {
            constructorModel.Entity<ClientPet>(entity =>
            {
                entity.ToTable("tbPet");
                entity.HasKey(c => c.Id).HasName("id");
                entity.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(c => c.Name).HasColumnName("name").HasMaxLength(100);
                entity.Property(c => c.Type).HasColumnName("type").HasMaxLength(50);
                entity.Property(c => c.Gender).HasColumnName("gender").HasMaxLength(2);
                entity.Property(c => c.Color).HasColumnName("color").HasMaxLength(30);
                entity.Property(c => c.Birthday).HasColumnName("birthday").HasMaxLength(20);
                entity.Property(c => c.Owner).HasColumnName("owner").HasMaxLength(100);
                entity.Property(c => c.Breed).HasColumnName("breed").HasMaxLength(50);
            });
        }


    }
}
