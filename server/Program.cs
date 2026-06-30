using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<StationService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TerminalJwtService>();
builder.Services.AddScoped<UserJwtService>();

// This could be change by BCryptPasswordHashear implementing the same interface 
builder.Services.AddScoped<
    IPasswordHasher<Restaurant>, 
    PasswordHasher<Restaurant>
>();

builder.Services.AddDbContext<AppDbContext>(
    options => options
        .UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
