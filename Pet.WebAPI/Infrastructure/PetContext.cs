using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Entities.Enums;
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
        public DbSet<Genero> Generos { get; set; }
        public DbSet<TamanhoPet> TamanhosPet { get; set; }
        public DbSet<Pets>? Pets { get; set; }
        public DbSet<EnderecoPrestador>? EnderecosPrestadores { get; set; }
        public DbSet<Prestador>? Prestadores { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Servico>? Servicos { get; set; }
        public DbSet<UsuarioPrestador>? UsuariosPrestadores { get; set; }
        public DbSet<ServicoPrestador>? ServicosPrestador { get; set; }
        public DbSet<EnderecoCliente>? EnderecosClientes { get; set; }
        //public DbSet<UsuarioCliente>? UsuariosClientes { get; set; }

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
            constructorModel.HasDefaultSchema("dbo");

            constructorModel
                .Entity<Genero>().HasData(
                    Enum.GetValues(typeof(EnumGenero))
                    .Cast<EnumGenero>()
                    .Select(e => new Genero()
                    {
                        GeneroId = e,
                        Descricao = e.ToString()
                    })
                    );

            constructorModel
            .Entity<TamanhoPet>().HasData(
                Enum.GetValues(typeof(EnumTamanhoPet))
                .Cast<EnumTamanhoPet>()
                .Select(e => new TamanhoPet()
                {
                    TamanhoPetId = e,
                    Descricao = e.ToString()
                })
                );
            constructorModel
            .Entity<TipoPet>().HasData(
             Enum.GetValues(typeof(EnumTipoPet))
             .Cast<EnumTipoPet>()
             .Select(e => new TipoPet()
             {
                 TipoPetId = e,
                 Descricao = e.ToString()
             })
             );


        }
    }
}
