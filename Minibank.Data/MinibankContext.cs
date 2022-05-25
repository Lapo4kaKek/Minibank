using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Minibank.Data.Users;

namespace Minibank.Data
{
    public class MinibankContext : DbContext
    {
        public DbSet<UserDbModel> Users { get; set; }

        public MinibankContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MinibankContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Factory : IDesignTimeDbContextFactory<MinibankContext>
    {
        public MinibankContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder()
                .UseNpgsql("Host=localhost;Port=5432;Database=minibank;Username=postgres;Password=Wasalas2003")
                .Options;
            return new MinibankContext(options);
        }
    }
}