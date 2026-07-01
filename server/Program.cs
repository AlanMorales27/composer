using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    IPasswordHasher<Account>, 
    PasswordHasher<Account>
>();

builder.Services.AddScoped<
    IPasswordHasher<User>, 
    PasswordHasher<User>
>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization( options =>
{
    options.AddPolicy(
        "TerminalToken", 
        policy => policy.RequireClaim("token_type", "termina;")
    );
}
);

builder.Services.AddDbContext<AppDbContext>(
    options => options
        .UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
