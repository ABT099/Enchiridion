using Enchiridion.Api;
using Enchiridion.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opts =>
    {
        opts.UseNpgsql(connectionString: builder.Configuration.GetConnectionString("Default")!)
            .UseSnakeCaseNamingConvention();
    }, optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddDbContext<AuthDbContext>(opts =>
    {
        opts.UseNpgsql(connectionString: builder.Configuration.GetConnectionString("Auth")!);
    }, optionsLifetime: ServiceLifetime.Singleton);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();
