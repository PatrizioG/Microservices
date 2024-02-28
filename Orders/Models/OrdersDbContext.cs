using Microsoft.EntityFrameworkCore;

namespace Orders.Models;

public class OrdersDbContext : DbContext
{
    public DbSet<OrderEntity> Orders { get; set; }

    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
    {
    }
}