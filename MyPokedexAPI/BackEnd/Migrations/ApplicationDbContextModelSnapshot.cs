﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPokedexAPI.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyPokedexAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MyPokedexAPI.Models.Pack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("BronzeChance")
                        .HasColumnType("double precision");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("DiamondChance")
                        .HasColumnType("double precision");

                    b.Property<double>("GoldChance")
                        .HasColumnType("double precision");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<double>("PlatinumChance")
                        .HasColumnType("double precision");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<double>("SilverChance")
                        .HasColumnType("double precision");

                    b.Property<int>("TotalBought")
                        .HasColumnType("integer");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Packs");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.PackUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OpenedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PackId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PackId");

                    b.HasIndex("UserId");

                    b.ToTable("PackUsers");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseAttackPoints")
                        .HasColumnType("integer");

                    b.Property<int>("BaseDefensePoints")
                        .HasColumnType("integer");

                    b.Property<int>("BaseHealthPoints")
                        .HasColumnType("integer");

                    b.Property<int>("BaseSpeedPoints")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("RegionId")
                        .HasColumnType("integer");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("RegionId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.PokemonInPack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PackId")
                        .HasColumnType("integer");

                    b.Property<int>("PokemonId")
                        .HasColumnType("integer");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("PackId");

                    b.HasIndex("PokemonId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("PokemonInPacks");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.TotalDiamondPokemonsRanking", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.Property<int>("TotalDiamondPokemons")
                        .HasColumnType("integer");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("TotalDiamondPokemonsRankings");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.TotalPacksOpenedRanking", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.Property<int>("TotalPacksOpened")
                        .HasColumnType("integer");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("TotalPacksOpenedRankings");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("NextOpenExpected")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.UserPokemons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActualAttackPoints")
                        .HasColumnType("integer");

                    b.Property<int>("ActualDefensePoints")
                        .HasColumnType("integer");

                    b.Property<int>("ActualHealthPoints")
                        .HasColumnType("integer");

                    b.Property<int>("ActualSpeedPoints")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsFavourite")
                        .HasColumnType("boolean");

                    b.Property<int>("PackId")
                        .HasColumnType("integer");

                    b.Property<int>("PokemonId")
                        .HasColumnType("integer");

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalCombatPoints")
                        .HasColumnType("integer");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("PackId");

                    b.HasIndex("PokemonId");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("UserPokemons");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<decimal>("Money")
                        .HasColumnType("numeric");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.Pack", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.PackUsers", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.Pack", "Pack")
                        .WithMany("PackUsers")
                        .HasForeignKey("PackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "User")
                        .WithMany("PackUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pack");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.Pokemon", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("Region");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.PokemonInPack", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.Pack", "Pack")
                        .WithMany("PokemonInPacks")
                        .HasForeignKey("PackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.Pokemon", "Pokemon")
                        .WithMany("PokemonInPacks")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("Pack");

                    b.Navigation("Pokemon");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.Region", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.Role", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.TotalDiamondPokemonsRanking", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "User")
                        .WithOne("TotalDiamondPokemonsRanking")
                        .HasForeignKey("MyPokedexAPI.Models.TotalDiamondPokemonsRanking", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.TotalPacksOpenedRanking", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "User")
                        .WithOne("TotalPacksOpenedRanking")
                        .HasForeignKey("MyPokedexAPI.Models.TotalPacksOpenedRanking", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.UserPokemons", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.User", "CreatedByUser")
                        .WithMany("UserPokemonsCreatedBy")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.Pack", "Pack")
                        .WithMany("UserPokemons")
                        .HasForeignKey("PackId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.Pokemon", "Pokemon")
                        .WithMany("UserPokemons")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "UpdatedByUser")
                        .WithMany("UserPokemonsUpdatedBy")
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyPokedexAPI.Models.User", "User")
                        .WithMany("UserPokemons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("Pack");

                    b.Navigation("Pokemon");

                    b.Navigation("UpdatedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.UserProfile", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("MyPokedexAPI.Models.UserProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.UserRole", b =>
                {
                    b.HasOne("MyPokedexAPI.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPokedexAPI.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.Pack", b =>
                {
                    b.Navigation("PackUsers");

                    b.Navigation("PokemonInPacks");

                    b.Navigation("UserPokemons");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.Pokemon", b =>
                {
                    b.Navigation("PokemonInPacks");

                    b.Navigation("UserPokemons");
                });

            modelBuilder.Entity("MyPokedexAPI.Models.User", b =>
                {
                    b.Navigation("PackUsers");

                    b.Navigation("TotalDiamondPokemonsRanking")
                        .IsRequired();

                    b.Navigation("TotalPacksOpenedRanking")
                        .IsRequired();

                    b.Navigation("UserPokemons");

                    b.Navigation("UserPokemonsCreatedBy");

                    b.Navigation("UserPokemonsUpdatedBy");

                    b.Navigation("UserProfile")
                        .IsRequired();

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
