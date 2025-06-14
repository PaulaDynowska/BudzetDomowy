using Microsoft.EntityFrameworkCore;
using BudzetDomowyApp.Models;

namespace BudzetDomowyApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<PlanowanyWydatek> PlanowaneWydatki { get; set; }



        public DbSet<Operacja> Operacje { get; set; }
    }
}
