using Microsoft.EntityFrameworkCore;

public class AppDbContext: DbContext
{
    public DbSet<Station> Stations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    
}