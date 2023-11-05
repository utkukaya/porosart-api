﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using porosartapi.model;

#nullable disable

namespace porosartapi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231104230858_add_event")]
    partial class add_event
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("porosartapi.model.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_name");

                    b.Property<string>("ImageRatio")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_ratio");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.HasKey("Id")
                        .HasName("pk_games");

                    b.ToTable("games", (string)null);
                });

            modelBuilder.Entity("porosartapi.model.GameContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date");

                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<string>("HTMLCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("html_code");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.HasKey("Id")
                        .HasName("pk_game_content");

                    b.ToTable("game_content", (string)null);
                });

            modelBuilder.Entity("porosartapi.model.GameImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date");

                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_name");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.HasKey("Id")
                        .HasName("pk_game_images");

                    b.ToTable("game_images", (string)null);
                });

            modelBuilder.Entity("porosartapi.model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("porosartapi.model.TeamMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.HasKey("Id")
                        .HasName("pk_teams");

                    b.ToTable("teams", (string)null);
                });

            modelBuilder.Entity("porosartapi.model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_users_role_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("porosartapi.model.User", b =>
                {
                    b.HasOne("porosartapi.model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_role_role_id");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}