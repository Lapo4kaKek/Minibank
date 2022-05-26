using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Minibank.Data.Users;
using System;
using Minibank.Data.AccountsBank;

namespace Minibank.Data
{
    public class MinibankContext : DbContext
    {
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<AccountBankDbModel> Accounts { get; set; }
        public MinibankContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MinibankContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            //optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
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