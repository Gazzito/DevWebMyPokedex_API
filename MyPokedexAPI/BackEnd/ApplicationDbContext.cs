using Microsoft.EntityFrameworkCore;



public class ApplicationDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<TotalDiamondPokemonsRanking> TotalDiamondPokemonsRankings { get; set; }
    public DbSet<TotalPacksOpenedRanking> TotalPacksOpenedRankings { get; set; }
    public DbSet<UserPokemons> UserPokemons { get; set; }   
    public DbSet<Pack> Pack { get; set; } 
    public DbSet<PackUsers> PackUsers { get; set; } 
    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<PokemonInPack> PokemonInPack { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = true;
    }

    public ApplicationDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);







        //-----------------------------------------------Users------------------------------------------------------

        // Configure one-to-many relationship 
        modelBuilder.Entity<Pokemon>()
       .HasOne(p => p.Region)
       .WithMany(r => r.Pokemons)
       .HasForeignKey(p => p.RegionId);

        // Mandatory Fields
        modelBuilder.Entity<User>()
        .Property(p => p.Name)
        .IsRequired();
        modelBuilder.Entity<User>()
        .Property(p => p.Username)
        .IsRequired();
        modelBuilder.Entity<User>()
        .Property(p => p.Password)
        .IsRequired();
        modelBuilder.Entity<User>()
        .Property(p => p.Email)
        .IsRequired();
        modelBuilder.Entity<User>()
        .Property(p => p.MobilePhone)
        .IsRequired();

        //-----------------------------------------------Pokemon------------------------------------------------------



        // Configure one-to-many relationship between Pokemon and Region
        modelBuilder.Entity<Pokemon>()
            .HasOne(p => p.Region)
            .WithMany(r => r.Pokemons)
            .HasForeignKey(p => p.RegionId);

        // Configure one-to-many relationships for CreatedBy and UpdatedBy
        modelBuilder.Entity<Pokemon>()
            .HasOne(p => p.CreatedBy)
            .WithMany(u => u.CreatedPokemons)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Pokemon>()
            .HasOne(p => p.UpdatedBy)
            .WithMany(u => u.UpdatedPokemons)
            .HasForeignKey(p => p.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Mandatory Fields
        modelBuilder.Entity<Pokemon>()
       .Property(p => p.Name)
       .IsRequired();
        modelBuilder.Entity<Pokemon>()
        .Property(p => p.RegionId)
        .IsRequired();
        modelBuilder.Entity<Pokemon>()
        .Property(p => p.BaseAttackPoints)
        .IsRequired();
        modelBuilder.Entity<Pokemon>()
        .Property(p => p.BaseDefensePoints)
        .IsRequired();
        modelBuilder.Entity<Pokemon>()
        .Property(p => p.BaseHealthPoints)
        .IsRequired();
        modelBuilder.Entity<Pokemon>()
        .Property(p => p.BaseSpeedPoints)
        .IsRequired();

        //-----------------------------------------------Region------------------------------------------------------

        modelBuilder.Entity<Region>()
            .HasOne(r => r.CreatedBy)
            .WithMany(u => u.CreatedRegions)
            .HasForeignKey(r => r.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Region>()
            .HasOne(r => r.UpdatedBy)
            .WithMany(u => u.UpdatedRegions)
            .HasForeignKey(r => r.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Mandatory Fields
        modelBuilder.Entity<Region>()
        .Property(p => p.Name)
        .IsRequired();



        //-----------------------------------------------User_Role------------------------------------------------------

        modelBuilder.Entity<UserRole>()
                   .HasKey(ur => ur.Id);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Mandatory Fields

        modelBuilder.Entity<UserRole>()
         .Property(p => p.UserId)
         .IsRequired();

        modelBuilder.Entity<UserRole>()
         .Property(p => p.RoleId)
         .IsRequired();

        //-----------------------------------------------User_Profile------------------------------------------------------

        // Configure the relationships for UserProfile
        modelBuilder.Entity<UserProfile>()
            .HasOne(up => up.CreatedBy)
            .WithMany(u => u.UserProfiles)
            .HasForeignKey(up => up.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserProfile>()
            .HasOne(up => up.UpdatedBy)
            .WithMany()
            .HasForeignKey(up => up.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

            // Mandatory Fields

        modelBuilder.Entity<UserProfile>()
        .Property(p => p.Money)
        .IsRequired();

        modelBuilder.Entity<UserProfile>()
        .Property(p => p.FullName)
        .IsRequired();

        //-----------------------------------------------TotalDiamondPokemonsRankings------------------------------------------------------

        modelBuilder.Entity<TotalDiamondPokemonsRanking>()
                .HasOne(tdpr => tdpr.CreatedBy)
                .WithMany(u => u.TotalDiamondPokemonsRankings)
                .HasForeignKey(tdpr => tdpr.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TotalDiamondPokemonsRanking>()
            .HasOne(tdpr => tdpr.UpdatedBy)
            .WithMany()
            .HasForeignKey(tdpr => tdpr.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

            // Mandatory Fields

        modelBuilder.Entity<TotalDiamondPokemonsRanking>()
        .Property(p => p.TotalDiamondPokemons)
        .IsRequired();

        modelBuilder.Entity<TotalDiamondPokemonsRanking>()
        .Property(p => p.Rank)
        .IsRequired();
        //-----------------------------------------------TotalPacksOpenedRanking------------------------------------------------------


        modelBuilder.Entity<TotalPacksOpenedRanking>()
        .HasOne(tpor => tpor.CreatedBy)
        .WithMany(u => u.TotalPacksOpenedRankings)
        .HasForeignKey(tpor => tpor.CreatedById)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TotalPacksOpenedRanking>()
            .HasOne(tpor => tpor.UpdatedBy)
            .WithMany()
            .HasForeignKey(tpor => tpor.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

            // Mandatory Fields

            modelBuilder.Entity<TotalPacksOpenedRanking>()
             .Property(p => p.TotalPacksOpened)
             .IsRequired();

             modelBuilder.Entity<TotalPacksOpenedRanking>()
             .Property(p => p.Rank)
            .IsRequired();

         //-----------------------------------------------UserPokemons------------------------------------------------------
          // Configure the relationships for UserPokemons
            modelBuilder.Entity<UserPokemons>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPokemons)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserPokemons>()
                .HasOne(up => up.Pokemon)
                .WithMany()
                .HasForeignKey(up => up.PokemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserPokemons>()
                .HasOne(up => up.CreatedBy)
                .WithMany()
                .HasForeignKey(up => up.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserPokemons>()
                .HasOne(up => up.UpdatedBy)
                .WithMany()
                .HasForeignKey(up => up.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the enum mapping for Rarities
            modelBuilder.Entity<UserPokemons>()
                .Property(up => up.Rarity)
                .HasConversion<string>();
          
          // Mandatory Fields
          
          modelBuilder.Entity<UserPokemons>()
             .Property(p => p.UserId)
             .IsRequired();

             modelBuilder.Entity<UserPokemons>()
             .Property(p => p.PokemonId)
             .IsRequired();

             modelBuilder.Entity<UserPokemons>()
             .Property(p => p.ActualAttackPoints)
             .IsRequired();

             modelBuilder.Entity<UserPokemons>()
             .Property(p => p.ActualDefensePoints)
             .IsRequired();

             modelBuilder.Entity<UserPokemons>()
             .Property(p => p.ActualHealthPoints)
             .IsRequired();

             modelBuilder.Entity<UserPokemons>()
             .Property(p => p.ActualSpeedPoints)
             .IsRequired();

             modelBuilder.Entity<UserPokemons>()
             .Property(p => p.TotalCombatPoints)
             .IsRequired();

             modelBuilder.Entity<UserPokemons>()
             .Property(p => p.Rarity)
             .IsRequired();

             //-----------------------------------------------Pack------------------------------------------------------

            modelBuilder.Entity<Pack>()
                .HasOne(p => p.CreatedBy)
                .WithMany()
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pack>()
                .HasOne(p => p.UpdatedBy)
                .WithMany()
                .HasForeignKey(p => p.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pack>()
             .Property(p => p.Name)
             .IsRequired();

             modelBuilder.Entity<Pack>()
             .Property(p => p.Price)
             .IsRequired();

             modelBuilder.Entity<Pack>()
             .Property(p => p.Image)
             .IsRequired();

             modelBuilder.Entity<Pack>()
             .Property(p => p.BronzeChance)
             .IsRequired();

             modelBuilder.Entity<Pack>()
             .Property(p => p.SilverChance)
             .IsRequired();

             modelBuilder.Entity<Pack>()
             .Property(p => p.GoldChance)
             .IsRequired();

             modelBuilder.Entity<Pack>()
             .Property(p => p.PlatinumChance)
             .IsRequired();

             modelBuilder.Entity<Pack>()
             .Property(p => p.DiamondChance)
             .IsRequired();

            //-----------------------------------------------PackUsers------------------------------------------------------
            modelBuilder.Entity<PackUsers>()
                .HasOne(pu => pu.User)
                .WithMany(u => u.PackUsers)
                .HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PackUsers>()
                .HasOne(pu => pu.Pack)
                .WithMany(p => p.PackUsers)
                .HasForeignKey(pu => pu.PackId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PackUsers>()
             .Property(p => p.UserId)
             .IsRequired();

            modelBuilder.Entity<PackUsers>()
             .Property(p => p.PackId)
             .IsRequired();

             

            //-----------------------------------------------Transaction------------------------------------------------------

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Pack)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PackId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
             .Property(p => p.UserId)
             .IsRequired();

            modelBuilder.Entity<Transaction>()
             .Property(p => p.PackId)
             .IsRequired();

            //-----------------------------------------------PokemonInPack------------------------------------------------------

        modelBuilder.Entity<PokemonInPack>()
                .HasOne(pip => pip.Pack)
                .WithMany(p => p.PokemonInPacks)
                .HasForeignKey(pip => pip.PackId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PokemonInPack>()
                .HasOne(pip => pip.Pokemon)
                .WithMany()
                .HasForeignKey(pip => pip.PokemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PokemonInPack>()
                .HasOne(pip => pip.CreatedBy)
                .WithMany(u => u.PokemonInPacksCreated)
                .HasForeignKey(pip => pip.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PokemonInPack>()
                .HasOne(pip => pip.UpdatedBy)
                .WithMany(u => u.PokemonInPacksUpdated)
                .HasForeignKey(pip => pip.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

    }






}