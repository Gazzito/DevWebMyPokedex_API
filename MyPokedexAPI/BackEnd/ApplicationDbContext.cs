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
//-------------------------------------------------------------------------------------------


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