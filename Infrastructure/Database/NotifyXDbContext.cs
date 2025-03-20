using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class NotifyXDbContext : DbContext
{
    public NotifyXDbContext(DbContextOptions<NotifyXDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserAuthProvider> UserAuthProviders { get; set; }
    public DbSet<Description> Descriptions { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationExecution> NotificationExecutions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUsers(modelBuilder);
        ConfigureUserAuthProvider(modelBuilder);
        ConfigureRoles(modelBuilder);
        ConfigureDescriptions(modelBuilder);
        ConfigureNotifications(modelBuilder);
        ConfigureNotifyExecutions(modelBuilder);
        ConfifureNotificationMethods(modelBuilder);
        ConfigureNotificationTypes(modelBuilder);
        SeedDescriptions(modelBuilder);
        SeedUsers(modelBuilder);
        SeedRoles(modelBuilder);
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
                    .WithMany()
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
                entity
                    .HasMany(x => x.Notifications)
                    .WithOne(y => y.User)
                    .HasForeignKey(y => y.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
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

    private void ConfigureNotifications(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).UseIdentityColumn();
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.Subject).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Text).IsRequired().HasMaxLength(2000);
            entity.Property(x => x.NotificationMethodId).IsRequired().HasConversion<int>();
            entity.Property(x => x.NotificationTypeId).IsRequired().HasConversion<int>();
            entity.Property(x => x.NextNotificationExecutionId).IsRequired(false);
            entity.Property(x => x.EndDate).IsRequired(false);
            entity.Property(x => x.ExecutionStart).IsRequired();
            entity.HasQueryFilter(x => x.EndDate == null);

            entity.HasOne(e => e.NotificationMethod)
              .WithMany()
              .HasForeignKey(e => e.NotificationMethodId)
              .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.NotificationType)
              .WithMany()
              .HasForeignKey(e => e.NotificationTypeId)
              .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.NextNotificationExecution)
                .WithOne()
                .HasForeignKey<Notification>(x => x.NextNotificationExecutionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }

    private void ConfigureNotifyExecutions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationExecution>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).UseIdentityColumn();
            entity.Property(x => x.NotificationId).IsRequired();
            entity.Property(x => x.Result).IsRequired(false);
            entity.Property(x => x.ExecutionDate).IsRequired();
            entity.Property(x => x.EndDate).IsRequired(false);
            entity.Property(x => x.FailDescriptionId).IsRequired(false);
            entity.Property(x => x.CustomFailDescription).IsRequired(false).HasMaxLength(500);
        });
    }

    private void ConfifureNotificationMethods(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationMethod>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasConversion<int>();
            entity.Property(x => x.Name).IsRequired().HasMaxLength(50);

            entity.HasData(
             Enum.GetValues(typeof(NotificationMethodEnum))
                 .Cast<NotificationMethodEnum>()
                 .Select(e => new NotificationMethod
                 {
                     Id = e,
                     Name = e.ToString()
                 }));
        });
    }

    private void ConfigureNotificationTypes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasConversion<int>();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);

            entity.HasData(
                Enum.GetValues(typeof(NotificationTypeEnum))
                    .Cast<NotificationTypeEnum>()
                    .Select(e => new NotificationType
                    {
                        Id = e,
                        Name = e.ToString()
                    }));
        });
    }

    private void SeedDescriptions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Description>().HasData(
            new Description
            {
                Id = 1,
                Language = "pl",
                Value = "Administrator systemu"
            });
    }

    private void SeedUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "",
                Name = "Admin",
                CreatedAt = new DateTime(2025, 3, 19, 0, 0, 0, DateTimeKind.Utc)
            });
    }

    private void SeedRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = RoleEnum.Admin,
                Name = "Admin",
                DescriptionId = 1
            });
    }
}