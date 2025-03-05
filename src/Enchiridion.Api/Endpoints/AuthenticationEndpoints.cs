using Enchiridion.Api.Services;
using Microsoft.AspNetCore.Identity;

namespace Enchiridion.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static void AddAuthenticationEndpoints(this WebApplication api)
    {
        api.MapPost("auth/login", HandleLogin).AllowAnonymous();
        api.MapPost("auth/register", HandleRegister).AllowAnonymous();
        api.MapPost("auth/logout", HandleLogout);
    }

    private static async Task<IResult> HandleLogin(
        LoginRequest request,
        AppDbContext db,
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

        var userInfo = await db.Users
            .AsNoTracking()
            .Select(x => new { x.Id, x.AuthId })
            .FirstOrDefaultAsync(x => x.AuthId == authUser.Id);

        if (userInfo is null)
        {
            return Results.NotFound("There is no user with the given username.");
        }
        
        var roles = await userManager.GetRolesAsync(authUser);

        if (roles.Count > 1)
        {
            return Results.BadRequest("User have more than one role. please contact support.");
        }

        var token = TokenService.GenerateToken(userInfo.Id, roles.First());

        return Results.Ok(token);
    }

    private static async Task<IResult> HandleRegister(
        RegisterRequest request,
        AppDbContext db,
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

        var token = TokenService.GenerateToken(user.Id, EnchiridionConstants.Roles.User);

        return Results.Ok(token);
    }

    

    private static IResult HandleLogout(HttpContext context)
    {
        try
        {
            var token = TokenService.GetTokenFromContext(context);
            
            EnchiridionConstants.BlackList.Add(token);

            return Results.Ok();
        }
        catch (TokenService.InvalidTokenException e)
        {
            Console.WriteLine(e);
            return Results.Unauthorized();
        }
    }
}