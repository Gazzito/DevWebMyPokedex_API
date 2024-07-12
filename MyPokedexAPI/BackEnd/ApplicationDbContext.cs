    using Microsoft.EntityFrameworkCore;



    public class ApplicationDbContext : DbContext
    {
        
        
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<User_Role> User_Role { get; set; }
    public DbSet<Pack> Pack { get; set; }
    public DbSet<Region> Region { get; set; }
    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<UserPokemons> UserPokemons { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<PackUsers> PackUsers { get; set; }
    
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

    //--------dados obrigatorios de inserir na tabela USER--------------------------------------
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
    //------------------------------------------------------------------------------------------
    //--------dados obrigatorios de inserir na tabela Pack--------------------------------------
    modelBuilder.Entity<Pack>()
                .Property(p => p.Name)
                .IsRequired();    
                modelBuilder.Entity<Pack>()
                .Property(p => p.Price)
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
                    modelBuilder.Entity<Pack>()
                .Property(p => p.TotalBought)
                .IsRequired();
    //--------------------------------------------------------------------------------------------
    //--------dados obrigatorios de inserir na tabela Region--------------------------------------
                    modelBuilder.Entity<Region>()
                .Property(p => p.Name)
                .IsRequired();    
    //--------------------------------------------------------------------------------------------
    //--------dados obrigatorios de inserir na tabela Pokemon--------------------------------------
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
 //--------------------------------------------------------------------------------------------
 //--------dados obrigatorios de inserir na tabela Role--------------------------------------
                modelBuilder.Entity<Role>()
                .Property(p => p.Name)
                .IsRequired();
                modelBuilder.Entity<Role>()
                .Property(p => p.Persistent)
                .IsRequired();
                modelBuilder.Entity<Role>()
                .Property(p => p.Ss_Key)
                .IsRequired();
                 modelBuilder.Entity<Role>()
                .Property(p => p.Espace_Id)
                .IsRequired();
//--------------------------------------------------------------------------------------------
 //--------dados obrigatorios de inserir na tabela User_Role--------------------------------------
                modelBuilder.Entity<User_Role>()
                .Property(p => p.UserId)
                .IsRequired();
                modelBuilder.Entity<User_Role>()
                .Property(p => p.RoleId)
                .IsRequired();
 //------------------------dados obrigatorio de inserir na tabela Transaction--------------------------------
                 modelBuilder.Entity<Transaction>()
                .Property(p => p.UserId)
                .IsRequired();
                modelBuilder.Entity<Transaction>()
                .Property(p => p.PackId)
                .IsRequired();
//------------------------dados obrigatorio de inserir na tabela PackUsers--------------------------------
                modelBuilder.Entity<PackUsers>()
                .Property(p => p.UserId)
                .IsRequired();
                modelBuilder.Entity<PackUsers>()
                .Property(p => p.PackId)
                .IsRequired();
//------------------------dados obrigatorio de inserir na tabela UserPokemons--------------------------------
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
                modelBuilder.Entity<UserPokemons>()
                .Property(p => p.IsFavourite)
                .IsRequired();
//------------------------dados obrigatorio de inserir na tabela PokemonInpACK--------------------------------
                modelBuilder.Entity<PokemonInPack>()
                .Property(p => p.PackId)
                .IsRequired();
                modelBuilder.Entity<PokemonInPack>()
                .Property(p => p.PokemonId)
                .IsRequired();
//------------------------dados obrigatorio de inserir na tabela Rarities--------------------------------
                 modelBuilder.Entity<Rarities>()
                .Property(p => p.Label)
                .IsRequired();
                modelBuilder.Entity<Rarities>()
                .Property(p => p.Order)
                .IsRequired();
                 modelBuilder.Entity<Rarities>()
                .Property(p => p.Is_Active)
                .IsRequired();
//------------------------dados obrigatorio de inserir na tabela UserProfile--------------------------------
                modelBuilder.Entity<UserProfile>()
                .Property(p => p.Money)
                .IsRequired();
                 modelBuilder.Entity<UserProfile>()
                .Property(p => p.FullName)
                .IsRequired();
//------------------------dados obrigatorio de inserir na tabela TotalDiamondPokemonRanking--------------------------------
                modelBuilder.Entity<UserProfile>()
                .Property(p => p.Money)
                .IsRequired();
                 modelBuilder.Entity<UserProfile>()
                .Property(p => p.FullName)
                .IsRequired();
//------------------------dados obrigatorio de inserir na tabela TotalPacksOpenedRanking--------------------------------                
                modelBuilder.Entity<TotalPacksOpenedRanking>()
                .Property(p => p.UserId)
                .IsRequired();
                 modelBuilder.Entity<TotalPacksOpenedRanking>()
                .Property(p => p.Quantity)
                .IsRequired();




// relacoes entre tabelas


         modelBuilder.Entity<UserPokemons>()
          .HasOne(p => p.CreatedBy)
         .WithMany()
          .HasForeignKey(p => p.CreatedBy)
          .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserPokemons>()
            .HasOne(p => p.UpdatedBy)
         .WithMany()
         .HasForeignKey(p => p.UpdatedBy)
         .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Region>()
         .HasOne(r => r.CreatedBy)
          .WithMany()
         .HasForeignKey(r => r.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Region>()
          .HasOne(r => r.UpdatedBy)
          .WithMany()
         .HasForeignKey(r => r.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Pokemon>()
         .HasOne(p => p.CreatedBy)
         .WithMany()
          .HasForeignKey(p => p.CreatedBy)
          .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Pokemon>()
       .HasOne(p => p.UpdatedBy)
         .WithMany()
         .HasForeignKey(p => p.UpdatedBy)
         .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<PackUsers>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PackUsers>()
            .HasOne(p => p.Pack)
            .WithMany()
            .HasForeignKey(p => p.PackId)
            .OnDelete(DeleteBehavior.Restrict);

            




  /* definir relações entre tabelas
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

         modelBuilder.Entity<Login>()
            .HasIndex(l => l.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Login)
            .WithOne(l => l.User)
            .HasForeignKey<Login>(l => l.UserId);
            


        modelBuilder.Entity<Friendship>()
        .HasOne(f => f.User)
        .WithMany(u => u.Friendships)
        .HasForeignKey(f => f.CreatedBy)
        .OnDelete(DeleteBehavior.Restrict);
        
    // Add a unique constraint for the combination of USERID and FRIENDID
        modelBuilder.Entity<Friendship>()
            .HasIndex(f => new { f.CreatedBy, f.FriendId })
            .IsUnique();


modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.CreatedBy);

        modelBuilder.Entity<UserChat>()
        .HasKey(uc => new { uc.UserID, uc.ChatID });

    modelBuilder.Entity<UserChat>()
        .HasOne(uc => uc.User)
        .WithMany(u => u.UserChats)
        .HasForeignKey(uc => uc.UserID);

    modelBuilder.Entity<UserChat>()
        .HasOne(uc => uc.Chat)
        .WithMany(c => c.UserChats)
        .HasForeignKey(uc => uc.ChatID);

    modelBuilder.Entity<Message>()
        .HasOne(m => m.User)
        .WithMany(u => u.Messages)
        .HasForeignKey(m => m.UserID);

    modelBuilder.Entity<Message>()
        .HasOne(m => m.Chat)
        .WithMany(c => c.Messages)
        .HasForeignKey(m => m.ChatID);    
    */
    }



    


}