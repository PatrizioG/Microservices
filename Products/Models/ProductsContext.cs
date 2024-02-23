using Microsoft.EntityFrameworkCore;

namespace Products.Models
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) { }
    }
}
