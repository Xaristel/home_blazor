using Microsoft.EntityFrameworkCore;
using home_blazor.Data.Model;

namespace home_blazor.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Finances;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<Income> Income { get; set; }
        public DbSet<Cost> Cost { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
