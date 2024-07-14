using Microsoft.EntityFrameworkCore;  // Importa o namespace para funcionalidades do Entity Framework Core
using MyPokedexAPI.Models;  // Importa o namespace para os modelos da aplicação

namespace MyPokedexAPI.Data  // Define o namespace para o contexto da base de dados
{
    public class ApplicationDbContext : DbContext  // Define a classe ApplicationDbContext que herda de DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)  // Construtor que aceita opções do DbContext
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }  // Define a DbSet para a entidade User
        public DbSet<UserProfile> UserProfiles { get; set; }  // Define a DbSet para a entidade UserProfile
        public DbSet<TotalDiamondPokemonsRanking> TotalDiamondPokemonsRankings { get; set; }  // Define a DbSet para a entidade TotalDiamondPokemonsRanking
        public DbSet<TotalPacksOpenedRanking> TotalPacksOpenedRankings { get; set; }  // Define a DbSet para a entidade TotalPacksOpenedRanking
        public DbSet<Role> Roles { get; set; }  // Define a DbSet para a entidade Role
        public DbSet<UserRole> UserRoles { get; set; }  // Define a DbSet para a entidade UserRole
        public DbSet<Pack> Packs { get; set; }  // Define a DbSet para a entidade Pack
        public DbSet<Region> Regions { get; set; }  // Define a DbSet para a entidade Region
        public DbSet<Pokemon> Pokemons { get; set; }  // Define a DbSet para a entidade Pokemon
        public DbSet<PackUsers> PackUsers { get; set; }  // Define a DbSet para a entidade PackUsers
        public DbSet<PokemonInPack> PokemonInPacks { get; set; }  // Define a DbSet para a entidade PokemonInPack
        public DbSet<UserPokemons> UserPokemons { get; set; }  // Define a DbSet para a entidade UserPokemons

        protected override void OnModelCreating(ModelBuilder modelBuilder)  // Método para configurar o modelo de dados
        {
            base.OnModelCreating(modelBuilder);

            // Configura o relacionamento um-para-um entre User e UserProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.Id);

            // Configura o relacionamento um-para-um entre User e TotalDiamondPokemonsRanking
            modelBuilder.Entity<User>()
                .HasOne(u => u.TotalDiamondPokemonsRanking)
                .WithOne(tdpr => tdpr.User)
                .HasForeignKey<TotalDiamondPokemonsRanking>(tdpr => tdpr.Id);

            // Configura o relacionamento um-para-um entre User e TotalPacksOpenedRanking
            modelBuilder.Entity<User>()
                .HasOne(u => u.TotalPacksOpenedRanking)
                .WithOne(tpor => tpor.User)
                .HasForeignKey<TotalPacksOpenedRanking>(tpor => tpor.Id);

            // Configura o relacionamento muitos-para-muitos entre User e Role através de UserRole
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);

            // Configura o relacionamento muitos-para-muitos entre User e Pack através de PackUsers
            modelBuilder.Entity<PackUsers>()
                .HasOne(pu => pu.User)
                .WithMany(u => u.PackUsers)
                .HasForeignKey(pu => pu.UserId);

            modelBuilder.Entity<PackUsers>()
                .HasOne(pu => pu.Pack)
                .WithMany(p => p.PackUsers)
                .HasForeignKey(pu => pu.PackId);

            // Configura o relacionamento muitos-para-muitos entre Pack e Pokemon através de PokemonInPack
            modelBuilder.Entity<PokemonInPack>()
                .HasOne(pip => pip.Pack)
                .WithMany(p => p.PokemonInPacks)
                .HasForeignKey(pip => pip.PackId);

            modelBuilder.Entity<PokemonInPack>()
                .HasOne(pip => pip.Pokemon)
                .WithMany(p => p.PokemonInPacks)
                .HasForeignKey(pip => pip.PokemonId);

            // Configura o relacionamento muitos-para-muitos entre User e Pokemon através de UserPokemons
            modelBuilder.Entity<UserPokemons>()
                .HasOne(up => up.Pack)
                .WithMany(p => p.UserPokemons)
                .HasForeignKey(up => up.PackId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserPokemons>()
                .HasOne(up => up.Pokemon)
                .WithMany(p => p.UserPokemons)
                .HasForeignKey(up => up.PokemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserPokemons>()
                .HasOne(up => up.CreatedByUser)
                .WithMany(u => u.UserPokemonsCreatedBy)
                .HasForeignKey(up => up.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserPokemons>()
                .HasOne(up => up.UpdatedByUser)
                .WithMany(u => u.UserPokemonsUpdatedBy)
                .HasForeignKey(up => up.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
