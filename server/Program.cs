using Microsoft.EntityFrameworkCore;
using Server.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<StationService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddDbContext<AppDbContext>(
    options => options
        .UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information)
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
