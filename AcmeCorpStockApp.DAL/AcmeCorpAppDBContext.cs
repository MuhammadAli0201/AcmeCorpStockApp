using AcmeCorpStockApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorpStockApp.DAL
{
    public class AcmeCorpAppDBContext : DbContext
    {
        public AcmeCorpAppDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<StockAppUser> StockAppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
