using Enchiridion.Api.ViewModels;
using AuthorRequest = Enchiridion.Api.Requests.AuthorRequest;

namespace Enchiridion.Api.Endpoints;

public static class AuthorEndpoints
{
    public static void AddAuthorEndpoints(this IEndpointRouteBuilder api)
    {
        api.MapGet("authors", GetAll);
        api.MapGet("authors/{id:int}", GetById);
        api.MapPost("authors", Create);
        api.MapPost("authors/link-user/{userId:int}", LinkToUser);
        api.MapPut("authors/{id:int}", Update);
        api.MapDelete("authors/{id:int}", Delete);
    }

    private static async Task<IResult> GetAll(AppDbContext db)
    {
        var authors = await db.Authors
            .Select(AuthorViewModels.FlatProjection)
            .ToListAsync();
        
        return Results.Ok(authors);
    }

    private static async Task<IResult> GetById(int id, AppDbContext db)
    {
        var author = await db.Authors
            .Include(x => x.Quotes)
            .Where(x => x.Id == id)
            .Select(AuthorViewModels.Projection)
            .FirstOrDefaultAsync();
        
        return author is null 
            ? Results.NotFound()
            : Results.Ok(author);
    }

    private static async Task<IResult> Create(AuthorRequest request, AppDbContext db)
    {
        var author = new Author
        {
            Name = request.Name,
            Description = request.Description
        };
        
        db.Authors.Add(author);
        await db.SaveChangesAsync();

        return Results.Ok();
    }

    private static async Task<IResult> LinkToUser(int userId, AppDbContext db)
    {
        var user = await db.Users.FindAsync(userId);

        if (user is null)
        {
            return Results.NotFound();
        }

        var author = new Author
        {
            UserId = userId,
            Name = user.FirstName + " " + user.LastName
        };
        
        db.Authors.Add(author);
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }
    
    private static async Task<IResult> Update(int id, AuthorRequest request, AppDbContext db)
    {
        var author = await db.Authors.FirstOrDefaultAsync(x => x.Id == id);

        if (author is null)
        {
            return Results.NotFound();
        }

        if (author.UserId is not null)
        {
            return Results.BadRequest("Author is linked to a user, update the user info to update the author");
        }
        
        var updated = false;

        if (author.Name != request.Name)
        {
            author.Name = request.Name;
            updated = true;
        }

        if (author.Description != request.Description)
        {
            author.Description = request.Description;
            updated = true;
        }
        
        if (updated is false)
        {
            return Results.BadRequest("No Updates Detected");
        }
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    
    private static async Task<IResult> Delete(int id, AppDbContext db)
    {
        var author = await db.Authors.FirstOrDefaultAsync(x => x.Id == id);

        if (author is null)
        {
            return Results.NotFound();
        }
        
        db.Authors.Remove(author);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}