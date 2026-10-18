using Microsoft.EntityFrameworkCore;
using MeuProjeto.API.Data;
using MeuProjeto.API.Interfaces;
using MeuProjeto.API.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });
    
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string DefaultConnection não configurada.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ISoftwareService, SoftwareService>();
builder.Services.AddScoped<IVersaoSoftwareService, VersaoSoftwareService>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    var logger = scope.ServiceProvider
        .GetRequiredService<ILogger<Program>>();

    var tentativas = 10;

    for (var i = 1; ; i++)
    {
        try
        {
            await db.Database.MigrateAsync();

            logger.LogInformation(
                "Banco de dados e migrations verificados com sucesso.");

            break;
        }
        catch (Exception ex) when (i < tentativas)
        {
            logger.LogWarning(
                ex,
                "Banco indisponível. Tentativa {Tentativa}/{Total}.",
                i, tentativas);

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();