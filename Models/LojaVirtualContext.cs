using Microsoft.EntityFrameworkCore;

namespace loja_virtual.Models
{
    public class LojaVirtualContext : DbContext
    {
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) : base(options) { }
        public DbSet<Category> Category { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }

    }
}