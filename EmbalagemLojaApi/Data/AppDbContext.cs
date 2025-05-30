using Microsoft.EntityFrameworkCore;
using EmbalagemLojaApi.Models;


namespace EmbalagemLojaApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Caixa> Caixas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento Pedido -> Produto (1:N)
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Produtos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Produto com Dimensões complexas (owned type)
            modelBuilder.Entity<Produto>()
                .OwnsOne(p => p.Dimensoes);
        }
    }
}
