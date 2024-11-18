using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var connectionString = "Server=localhost;Database=web_project;User=hoenmike;Password=Crtm123123;";
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
      }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      // Explicitly set key lengths for Identity tables
      builder.Entity<IdentityRole>(entity =>
      {
        entity.HasKey(r => r.Id);
        entity.Property(m => m.Id).HasMaxLength(128).IsRequired();
        entity.Property(m => m.Name).HasMaxLength(256);
        entity.Property(m => m.NormalizedName).HasMaxLength(256);
      });

      builder.Entity<IdentityUser>(entity =>
      {
        entity.Property(m => m.Id).HasMaxLength(128);
      });

      builder.Entity<IdentityUserLogin<string>>(entity =>
      {
        entity.Property(m => m.LoginProvider).HasMaxLength(128);
        entity.Property(m => m.ProviderKey).HasMaxLength(128);
      });

      builder.Entity<IdentityUserRole<string>>(entity =>
      {
        entity.Property(m => m.UserId).HasMaxLength(128);
        entity.Property(m => m.RoleId).HasMaxLength(128);
      });

      builder.Entity<IdentityUserToken<string>>(entity =>
      {
        entity.Property(m => m.UserId).HasMaxLength(128);
        entity.Property(m => m.LoginProvider).HasMaxLength(128);
        entity.Property(m => m.Name).HasMaxLength(128);
      });
    }
  }
}