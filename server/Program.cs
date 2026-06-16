var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<StationService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
