using Microsoft.EntityFrameworkCore;

namespace loja_virtual.Models
{
    public class LojaVirtualContext : DbContext
    {
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) : base(options) { }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Preco> Preco { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<VendaProduto> VendaProduto { get; set; }

    }
}