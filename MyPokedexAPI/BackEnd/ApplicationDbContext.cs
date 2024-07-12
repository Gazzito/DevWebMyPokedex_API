    using Microsoft.EntityFrameworkCore;



    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
    
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