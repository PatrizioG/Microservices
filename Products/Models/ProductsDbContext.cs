using Microsoft.EntityFrameworkCore;

namespace Products.Models
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }
    }
}
