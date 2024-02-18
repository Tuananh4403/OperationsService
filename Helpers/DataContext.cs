namespace OperationsService.Helpers;

using Microsoft.EntityFrameworkCore;
using OperationsService.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Role { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<UserRole>()
        .HasKey(ur => new { ur.UserId, ur.RoleId });

    modelBuilder.Entity<UserRole>()
        .HasOne(ur => ur.User)
        .WithMany(u => u.UserRoles)
        .HasForeignKey(ur => ur.UserId);

    modelBuilder.Entity<UserRole>()
        .HasOne(ur => ur.Role)
        .WithMany(r => r.UserRoles)
        .HasForeignKey(ur => ur.RoleId);
}
    
}