using Microsoft.EntityFrameworkCore;

public class AppDbContext: DbContext
{
    public DbSet<Station> Stations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer("Server=MSI\\SQLEXPRESS;Database=composerDb;Trusted_Connection=True;TrustServerCertificate=True;")
            .LogTo(Console.WriteLine, LogLevel.Information);
}