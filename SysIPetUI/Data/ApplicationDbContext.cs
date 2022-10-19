using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SysIPetUI.Models;

namespace SysIPetUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SysIPetUI.Models.AspNetTipoUsuario>? AspNetTipoUsuario { get; set; }
        public DbSet<SysIPetUI.Models.PetsViewModel> PetsViewModel { get; set; }
        public DbSet<SysIPetUI.Models.ClienteViewModel> ClienteViewModel { get; set; }
        public DbSet<SysIPetUI.Models.EnderecoClienteViewModel> EnderecoClienteViewModel { get; set; }
        public DbSet<SysIPetUI.Models.PetsListViewModel> PetsDropdownListViewModel { get; set; }
        public DbSet<SysIPetUI.Models.PetsListItem> PetsListItem { get; set; }
    }
}