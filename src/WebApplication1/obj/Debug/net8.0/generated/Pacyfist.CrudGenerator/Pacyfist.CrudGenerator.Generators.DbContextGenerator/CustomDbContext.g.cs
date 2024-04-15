namespace WebApplication1;

using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;


public partial class CustomDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Category> Categories { get; set; }

    public CustomDbContext(DbContextOptions<CustomDbContext> options)
        : base(options)
    {
    }
}