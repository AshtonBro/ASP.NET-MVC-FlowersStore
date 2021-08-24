using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FlowersStore.DataAccess.MSSQL.Entities;
using FlowersStore.DataAccess.MSSQL.Configurations;

namespace FlowersStore.DataAccess.MSSQL
{
    public class FlowersStoreDbContext : IdentityDbContext<User, UserRole, Guid>
    {
        public FlowersStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCard> ProductCards { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BasketConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCardConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}