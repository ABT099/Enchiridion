using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Enchiridion.Api;

public static class DependencyInjection
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseNpgsql(connectionString: builder.Configuration.GetConnectionString("Default")!)
                .UseSnakeCaseNamingConvention();
        }, optionsLifetime: ServiceLifetime.Singleton);

        builder.Services.AddAuth(connectionString: builder.Configuration.GetConnectionString("Auth")!);
    }

    private static void AddAuth(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AuthDbContext>(opts =>
            opts.UseNpgsql(connectionString),
            optionsLifetime: ServiceLifetime.Singleton);

        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>();

        services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "jwt";
                o.DefaultChallengeScheme = "jwt";
            })
            .AddJwtBearer("jwt", o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        var authorizationHeader = ctx.Request.Headers.Authorization.ToString();
                        if (string.IsNullOrWhiteSpace(authorizationHeader) ||
                            !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            return Task.CompletedTask;
                        }

                        var token = authorizationHeader["Bearer ".Length..].Trim();

                        if (string.IsNullOrWhiteSpace(token))
                        {
                            return Task.CompletedTask;
                        };

                        if (EnchiridionConstants.BlackList.Contains(token))
                        {
                            ctx.Fail("This token has been invalidated.");
                            return Task.CompletedTask;
                        }

                        ctx.Token = token;
                        return Task.CompletedTask;
                    }
                };

                o.Configuration = new OpenIdConnectConfiguration
                {
                    SigningKeys =
                    {
                        new RsaSecurityKey(EnchiridionConstants.Keys.RsaKey)
                    }
                };

                o.MapInboundClaims = false;
            });
    }
}