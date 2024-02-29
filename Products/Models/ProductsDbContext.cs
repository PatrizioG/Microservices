using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Products.Models;


public class ProductsDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }
}