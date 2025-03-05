using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Enchiridion.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static void AddAuthenticationEndpoints(this WebApplication api)
    {
        api.MapPost("auth/login", HandleLogin);
        api.MapPost("auth/register", HandleRegister);
        api.MapPost("auth/logout", HandleLogout);
    }

    private static async Task<IResult> HandleLogin(
        LoginRequest request,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        var authUser = await userManager.FindByNameAsync(request.Username);

        if (authUser is null)
        {
            return Results.NotFound("There is no user with the given username.");
        }

        var result = await signInManager.PasswordSignInAsync(authUser, request.Password, false, false);

        if (result.Succeeded is false)
        {
            return Results.Unauthorized();
        }

        var roles = await userManager.GetRolesAsync(authUser);

        if (roles.Count > 1)
        {
            return Results.BadRequest("User have more than one role. please contact support.");
        }

        var token = GenerateToken(authUser.Id, roles.First());

        return Results.Ok(token);
    }

    private static async Task<IResult> HandleRegister(
        AppDbContext db,
        RegisterRequest request,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        var identityUser = await userManager.FindByNameAsync(request.UserName);

        if (identityUser is not null)
        {
            return Results.Conflict("User already exists");
        }

        identityUser = new IdentityUser
        {
            UserName = request.UserName,
            Email = request.Email,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(identityUser, request.Password);

        var user = new User
        {
            AuthId = identityUser.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
        };

        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        await userManager.AddToRoleAsync(identityUser, EnchiridionConstants.Roles.User);

        var token = GenerateToken(identityUser.Id, EnchiridionConstants.Roles.User);

        return Results.Ok(token);
    }

    private static string GenerateToken(string authId, string role)
    {
        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(EnchiridionConstants.Claims.AuthId, authId));
        identity.AddClaim(new Claim(EnchiridionConstants.Claims.Role, role));

        var key = new RsaSecurityKey(EnchiridionConstants.Keys.RsaKey);

        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = "https://localhost:5001",
            Subject = identity,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
        });

        return token;
    }

    private static IResult HandleLogout(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(authorizationHeader) ||
            !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return Results.Unauthorized();
        }

        var token = authorizationHeader["Bearer ".Length..].Trim();

        if (string.IsNullOrWhiteSpace(token))
        {
            return Results.BadRequest("Invalid token.");
        }

        EnchiridionConstants.BlackList.Add(token);

        return Results.Ok();
    }
}