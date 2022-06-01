using MCProject.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MCProject.Database.Data;

public class MainContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<UserRoles> UserRoles { get; set; } = null!;

    public MainContext()
    {

    }

    public MainContext(DbContextOptions<MainContext> options) : base(options)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.GameName).IsUnique();
        });

        modelBuilder.Entity<UserRoles>(entity =>
        {
            entity.HasKey(r => new { r.UserId, r.RoleId });
        });
    }
}
