using Enchiridion.Api.Services;
using Enchiridion.Api.ViewModels;

namespace Enchiridion.Api.Endpoints;

public static class UserEndpoints
{
    public static void AddUserEndpoints(this RouteGroupBuilder api)
    {
        api.MapGet("users", GetAll)
            .RequireAuthorization(
                x => x.RequireRole(EnchiridionConstants.Roles.Admin)
            );
        
        api.MapGet("users/{id:int}", GetById)
            .RequireAuthorization(
                x => x.RequireRole(EnchiridionConstants.Roles.Admin)
            );

        api.MapGet("users/from-auth", GetFromAuth);
        api.MapPut("users", Update);
        api.MapDelete("users", Delete);
    }

    private static async Task<IResult> GetAll(AppDbContext db)
    {
        var users = await db.Users
            .AsNoTracking()
            .Select(UserViewModels.FlatProjection)
            .ToListAsync();

        return Results.Ok(users);
    }

    private static async Task<IResult> GetById(int id, AppDbContext db)
    {
        var user = await db.Users
            .AsNoTracking()
            .Select(UserViewModels.FlatProjection)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return user is null
            ? Results.NotFound()
            : Results.Ok(user);
    }

    private static async Task<IResult> GetFromAuth(HttpContext httpContext, AppDbContext db)
    {
        var id = TokenService.GetUserId(httpContext);
        
        var user = await db.Users
            .AsNoTracking()
            .Select(UserViewModels.FlatProjection)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return user is null
            ? Results.NotFound()
            : Results.Ok(user);
    }

    private static async Task<IResult> Update(UpdateUserRequest request, AppDbContext db, HttpContext httpContext)
    {
        var id = TokenService.GetUserId(httpContext);
        
        var user = await db.Users
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
        {
            return Results.NotFound();
        }
        
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.UserName = request.UserName;
        
        if (user.Author is not null)
        {
            user.Author.Name = request.FirstName + " " + request.LastName;
        }
        
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }

    private static async Task<IResult> Delete(HttpContext httpContext, AppDbContext db)
    {
        var id = TokenService.GetUserId(httpContext);
        
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
        {
            return Results.NotFound();
        }
        
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}