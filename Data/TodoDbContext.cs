namespace Data;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<User> Users => Set<User>();
    public DbSet<HistoryBite> HistoryBites => Set<HistoryBite>();
    public DbSet<DeliveryLog> DeliveryLogs => Set<DeliveryLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Id).ValueGeneratedOnAdd();
            e.Property(u => u.Email).HasMaxLength(256);
            e.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<HistoryBite>(e =>
        {
            e.HasKey(h => h.Id);
            e.Property(h => h.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<DeliveryLog>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.Id).ValueGeneratedOnAdd();
            e.HasOne(d => d.User)
             .WithMany()
             .HasForeignKey(d => d.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
