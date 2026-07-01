using Microsoft.EntityFrameworkCore;

public class AppDbContext: DbContext
{
    public DbSet<Account> Restaurants {get; set;}
    public DbSet<Station> Stations { get; set; }
    public DbSet<User> Users {get; set;}

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    
}