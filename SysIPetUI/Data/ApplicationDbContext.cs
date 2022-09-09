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
        public DbSet<SysIPetUI.Models.AgendamentoViewModel>? AgendamentoViewModel { get; set; }
    }
}