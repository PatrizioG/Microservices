using Microsoft.EntityFrameworkCore;

namespace Users.Models;

public class UsersDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }
}