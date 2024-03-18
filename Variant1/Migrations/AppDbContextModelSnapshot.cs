﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Variant1;

#nullable disable

namespace Variant1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DisplayPlant", b =>
                {
                    b.Property<int>("DisplaysId")
                        .HasColumnType("integer")
                        .HasColumnName("displays_id");

                    b.Property<int>("PlantsId")
                        .HasColumnType("integer")
                        .HasColumnName("plants_id");

                    b.HasKey("DisplaysId", "PlantsId")
                        .HasName("pk_display_plant");

                    b.HasIndex("PlantsId")
                        .HasDatabaseName("ix_display_plant_plants_id");

                    b.ToTable("display_plant", (string)null);
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer")
                        .HasColumnName("roles_id");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer")
                        .HasColumnName("users_id");

                    b.HasKey("RolesId", "UsersId")
                        .HasName("pk_user_user_role");

                    b.HasIndex("UsersId")
                        .HasDatabaseName("ix_user_user_role_users_id");

                    b.ToTable("user_user_role", (string)null);
                });

            modelBuilder.Entity("Variant1.Models.Display", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_displays");

                    b.ToTable("displays", (string)null);
                });

            modelBuilder.Entity("Variant1.Models.Plant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastWatering")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_watering");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<int>("ResponsibleId")
                        .HasColumnType("integer")
                        .HasColumnName("responsible_id");

                    b.HasKey("Id")
                        .HasName("pk_plants");

                    b.HasIndex("ResponsibleId")
                        .HasDatabaseName("ix_plants_responsible_id");

                    b.ToTable("plants", (string)null);
                });

            modelBuilder.Entity("Variant1.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DisplayId")
                        .HasColumnType("integer")
                        .HasColumnName("display_id");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("text");

                    b.HasKey("Id")
                        .HasName("pk_reviews");

                    b.HasIndex("DisplayId")
                        .HasDatabaseName("ix_reviews_display_id");

                    b.ToTable("reviews", (string)null);
                });

            modelBuilder.Entity("Variant1.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("login");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("password_hash");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Ivan",
                            LastName = "Ivanovich",
                            Login = "ivanko",
                            PasswordHash = "2UfAU+9jPtWKPu9esgC2MQ==.AZLXTe1DRj0lCQ6MW220DGzfviqASXErKzBhGUSTs7Q="
                        });
                });

            modelBuilder.Entity("Variant1.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("role");

                    b.HasKey("Id")
                        .HasName("pk_user_roles");

                    b.ToTable("user_roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Role = "User"
                        },
                        new
                        {
                            Id = 2,
                            Role = "Visitor"
                        },
                        new
                        {
                            Id = 3,
                            Role = "Gardener"
                        },
                        new
                        {
                            Id = 4,
                            Role = "Manager"
                        });
                });

            modelBuilder.Entity("Variant1.Models.Visitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayId")
                        .HasColumnType("integer")
                        .HasColumnName("display_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("VisitDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("visit_date_time");

                    b.HasKey("Id")
                        .HasName("pk_visitors");

                    b.HasIndex("DisplayId")
                        .HasDatabaseName("ix_visitors_display_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_visitors_user_id");

                    b.ToTable("visitors", (string)null);
                });

            modelBuilder.Entity("DisplayPlant", b =>
                {
                    b.HasOne("Variant1.Models.Display", null)
                        .WithMany()
                        .HasForeignKey("DisplaysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_display_plant_displays_displays_id");

                    b.HasOne("Variant1.Models.Plant", null)
                        .WithMany()
                        .HasForeignKey("PlantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_display_plant_plants_plants_id");
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.HasOne("Variant1.Models.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_user_role_user_roles_roles_id");

                    b.HasOne("Variant1.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_user_role_users_users_id");
                });

            modelBuilder.Entity("Variant1.Models.Plant", b =>
                {
                    b.HasOne("Variant1.Models.User", "Responsible")
                        .WithMany()
                        .HasForeignKey("ResponsibleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_plants_users_responsible_id");

                    b.Navigation("Responsible");
                });

            modelBuilder.Entity("Variant1.Models.Review", b =>
                {
                    b.HasOne("Variant1.Models.Display", null)
                        .WithMany("Reviews")
                        .HasForeignKey("DisplayId")
                        .HasConstraintName("fk_reviews_displays_display_id");
                });

            modelBuilder.Entity("Variant1.Models.Visitor", b =>
                {
                    b.HasOne("Variant1.Models.Display", "Display")
                        .WithMany("Visitors")
                        .HasForeignKey("DisplayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_visitors_displays_display_id");

                    b.HasOne("Variant1.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_visitors_users_user_id");

                    b.Navigation("Display");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Variant1.Models.Display", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("Visitors");
                });
#pragma warning restore 612, 618
        }
    }
}
