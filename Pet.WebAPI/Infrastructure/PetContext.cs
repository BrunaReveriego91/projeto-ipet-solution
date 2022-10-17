using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Entities.Enums;
using Pet.WebAPI.Domain.Settings;
using System.Reflection.Metadata;

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

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<TamanhoPet> TamanhosPet { get; set; }
        public DbSet<Pets> Pets { get; set; }
        public DbSet<EnderecoPrestador> EnderecosPrestadores { get; set; }
        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<UsuarioPrestador> UsuariosPrestadores { get; set; }
        public DbSet<ServicoPrestador> ServicosPrestador { get; set; }
        public DbSet<EnderecoCliente> EnderecosClientes { get; set; }
        //public DbSet<UsuarioCliente>? UsuariosClientes { get; set; }
        public DbSet<Agenda> Agendamentos { get; set; }
        public DbSet<ServicoAgenda> ServicosAgendamento { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var addedItems = this.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added).ToList();

            addedItems.ForEach(e =>
            {
                var data_cadastro = e.Entity.GetType().GetProperty("Data_Cadastro");

                if (data_cadastro != null)
                {
                    data_cadastro.SetValue(e.Entity, DateTime.Now);
                }
            });

            return base.SaveChangesAsync(cancellationToken);
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder
                .Entity<Genero>().HasData(
                    Enum.GetValues(typeof(EnumGenero))
                    .Cast<EnumGenero>()
                    .Select(e => new Genero()
                    {
                        GeneroId = e,
                        Descricao = e.ToString()
                    })
                    );

            modelBuilder
            .Entity<TamanhoPet>().HasData(
                Enum.GetValues(typeof(EnumTamanhoPet))
                .Cast<EnumTamanhoPet>()
                .Select(e => new TamanhoPet()
                {
                    TamanhoPetId = e,
                    Descricao = e.ToString()
                })
                );

            modelBuilder
            .Entity<TipoPet>().HasData(
             Enum.GetValues(typeof(EnumTipoPet))
             .Cast<EnumTipoPet>()
             .Select(e => new TipoPet()
             {
                 TipoPetId = e,
                 Descricao = e.ToString()
             })
             );

            // 1 Prestador pode ter muitos serviços porém não podem ser duplicados.
            // Melhoria: Faz o chaveamento por Endereço Prestador. "O Endereço do Prestador pode ter N-Serviços não repetíveis"
            modelBuilder
               .Entity<ServicoPrestador>()
               .HasIndex(e => new
               {
                   e.PrestadorId,
                   e.ServicoId
               }).IsUnique().HasDatabaseName("IDX_SERVPREST_PREST");

            // Dentro de uma mesma Agenda não podem ter ServicoAgenda repetidos para o mesmo Endereço do Prestador.
            modelBuilder
               .Entity<ServicoAgenda>()
               .HasIndex(e => new
               {
                   e.AgendaId,
                   e.ServicoId,
                   e.EnderecoPrestadorId
               }).IsUnique().HasDatabaseName("IDX_AGENDA_SRVAGENDA_ENDPREST");

            modelBuilder
               .Entity<UsuarioPrestador>()
               .HasOne(e => e.Usuario)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
               .Entity<UsuarioPrestador>()
               .HasOne(e => e.Prestador)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
               .Entity<Agenda>()
               .HasOne(e => e.Cliente)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
               .Entity<Agenda>()
               .HasOne(e => e.Prestador)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}