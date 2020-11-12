using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using System;
using System.Reflection;

namespace MoOnlineStore.Infrastructure
{
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductBrand> ProductsBrands { get; set; }
        public DbSet<ProductType> ProductsTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}











//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    const string ConnectionString = @"Data Source=DESKTOP-BCQAK1M\SQLEXPRESS;Initial Catalog=PersonallFinancial;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
//    optionsBuilder.UseSqlServer(ConnectionString);
//}