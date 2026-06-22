using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<StationService>();

builder.Services.AddDbContext<AppDbContext>(
    options => options
        .UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information)
);

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
