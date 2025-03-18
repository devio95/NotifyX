using Domain.Entities;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserAuthProvider> UserAuthProviders { get; set; }
    public DbSet<Description> Descriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUsers(modelBuilder);
        ConfigureUserAuthProvider(modelBuilder);
        ConfigureRoles(modelBuilder);
        ConfigureDescriptions(modelBuilder);
    }

    private void ConfigureUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).UseIdentityColumn();
            entity.Property(x => x.Email).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.CreatedAt).IsRequired();
            entity
                .HasOne(x => x.AuthProvider)
                .WithOne(y => y.User)
                .HasForeignKey<UserAuthProvider>(y => y.UserId);
            entity
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    j => j
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex("RoleId");
                    });
        });
    }

    private void ConfigureUserAuthProvider(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAuthProvider>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).UseIdentityColumn();
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.Provider).IsRequired().HasMaxLength(100);
            entity.Property(x => x.ProviderUserId).IsRequired().HasMaxLength(100);
            entity.HasIndex(x => new { x.Provider, x.ProviderUserId }).IsUnique();
            entity.HasIndex(x => x.UserId).IsUnique();
        });
    }

    private void ConfigureRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion<int>();
            entity.Property(e => e.Id).UseIdentityColumn();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.Property(e => e.DescriptionId).IsRequired();
        });
    }

    private void ConfigureDescriptions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Description>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Language });
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Language).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Value).IsRequired();
            entity.HasIndex(e => e.Language);
            entity.HasIndex(e => e.Id);
        });
    }
}