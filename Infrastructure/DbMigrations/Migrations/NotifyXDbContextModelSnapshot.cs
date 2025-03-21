﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DbMigrations.Migrations
{
    [DbContext(typeof(NotifyXDbContext))]
    partial class NotifyXDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Description", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Language")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("language");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id", "Language")
                        .HasName("pk_descriptions");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_descriptions_id");

                    b.HasIndex("Language")
                        .HasDatabaseName("ix_descriptions_language");

                    b.ToTable("descriptions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Language = "pl",
                            Value = "Administrator systemu"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("ExecutionStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("execution_start");

                    b.Property<long?>("NextNotificationExecutionId")
                        .HasColumnType("bigint")
                        .HasColumnName("next_notification_execution_id");

                    b.Property<int>("NotificationMethodId")
                        .HasColumnType("integer")
                        .HasColumnName("notification_method_id");

                    b.Property<int>("NotificationTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("notification_type_id");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("subject");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_notifications");

                    b.HasIndex("NextNotificationExecutionId")
                        .IsUnique()
                        .HasDatabaseName("ix_notifications_next_notification_execution_id");

                    b.HasIndex("NotificationMethodId")
                        .HasDatabaseName("ix_notifications_notification_method_id");

                    b.HasIndex("NotificationTypeId")
                        .HasDatabaseName("ix_notifications_notification_type_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_notifications_user_id");

                    b.ToTable("notifications", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.NotificationExecution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CustomFailDescription")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("custom_fail_description");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("execution_date");

                    b.Property<int?>("FailDescriptionId")
                        .HasColumnType("integer")
                        .HasColumnName("fail_description_id");

                    b.Property<bool>("IsProcessing")
                        .HasColumnType("boolean")
                        .HasColumnName("is_processing");

                    b.Property<int>("NotificationId")
                        .HasColumnType("integer")
                        .HasColumnName("notification_id");

                    b.Property<bool?>("Result")
                        .HasColumnType("boolean")
                        .HasColumnName("result");

                    b.HasKey("Id")
                        .HasName("pk_notification_executions");

                    b.HasIndex("NotificationId")
                        .HasDatabaseName("ix_notification_executions_notification_id");

                    b.ToTable("notification_executions", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.NotificationMethod", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_notification_method");

                    b.ToTable("notification_method", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Mail"
                        },
                        new
                        {
                            Id = 1,
                            Name = "SMS"
                        });
                });

            modelBuilder.Entity("Domain.Entities.NotificationType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_notification_type");

                    b.ToTable("notification_type", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Single"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Day"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Week"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Month"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Year"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Minute"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Users.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int>("DescriptionId")
                        .HasColumnType("integer")
                        .HasColumnName("description_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_roles_name");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 0,
                            DescriptionId = 1,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Utc),
                            Email = "",
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Users.UserAuthProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("provider");

                    b.Property<string>("ProviderUserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("provider_user_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_auth_providers");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_auth_providers_user_id");

                    b.HasIndex("Provider", "ProviderUserId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_auth_providers_provider_provider_user_id");

                    b.ToTable("user_auth_providers", (string)null);
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_role");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_role_role_id");

                    b.ToTable("user_role", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Notification", b =>
                {
                    b.HasOne("Domain.Entities.NotificationExecution", "NextNotificationExecution")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Notification", "NextNotificationExecutionId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_notifications_notification_executions_next_notification_exe");

                    b.HasOne("Domain.Entities.NotificationMethod", "NotificationMethod")
                        .WithMany()
                        .HasForeignKey("NotificationMethodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_notifications_notification_method_notification_method_id");

                    b.HasOne("Domain.Entities.NotificationType", "NotificationType")
                        .WithMany()
                        .HasForeignKey("NotificationTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_notifications_notification_type_notification_type_id");

                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_notifications_users_user_id");

                    b.Navigation("NextNotificationExecution");

                    b.Navigation("NotificationMethod");

                    b.Navigation("NotificationType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.NotificationExecution", b =>
                {
                    b.HasOne("Domain.Entities.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_notification_executions_notifications_notification_id");

                    b.Navigation("Notification");
                });

            modelBuilder.Entity("Domain.Entities.Users.UserAuthProvider", b =>
                {
                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithOne("AuthProvider")
                        .HasForeignKey("Domain.Entities.Users.UserAuthProvider", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_auth_providers_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("Domain.Entities.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_roles_role_id");

                    b.HasOne("Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_users_user_id");
                });

            modelBuilder.Entity("Domain.Entities.Users.User", b =>
                {
                    b.Navigation("AuthProvider");

                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
