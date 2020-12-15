using FlowersStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowersStore.Data
{
    public class StoreDBContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }  
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCart> ProductCarts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public StoreDBContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //default connection
            optionsBuilder.UseSqlServer(@"Data Source=ASHTON\ASHTON;Initial Catalog=FlowersStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        }

    }
}
