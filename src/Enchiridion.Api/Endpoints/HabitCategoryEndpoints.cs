using Enchiridion.Api.ViewModels;

namespace Enchiridion.Api.Endpoints;

public static class HabitCategoryEndpoints
{
    public static void AddHabitCategoryEndpoints(this RouteGroupBuilder api)
    {
        api.MapGet("habit-categories", GetAll);
        api.MapGet("habit-categories/{id:int}", GetById);
        api.MapPost("habit-categories", Create)
            .RequireAuthorization(
                x => x.RequireRole(EnchiridionConstants.Roles.Admin)
            );
        api.MapPut("habit-categories/{id:int}", Update)
            .RequireAuthorization(
                x => x.RequireRole(EnchiridionConstants.Roles.Admin)
            );
        api.MapDelete("habit-categories/{id:int}", Delete)
            .RequireAuthorization(
                x => x.RequireRole(EnchiridionConstants.Roles.Admin)
            );
    }

    private static async Task<IResult> GetAll(AppDbContext db)
    {
        var categories = await db.HabitCategories
            .AsNoTracking()
            .Select(HabitCategoryViewModels.Projection)
            .ToListAsync();
        
        return Results.Ok(categories);
    }

    private static async Task<IResult> GetById(int id, AppDbContext db)
    {
        var category = await db.HabitCategories
            .AsNoTracking()
            .Select(HabitCategoryViewModels.Projection)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return category is null
            ? Results.NotFound()
            : Results.Ok(category);
    }

    private static async Task<IResult> Create(HabitCategoryRequest request, AppDbContext db)
    {
        var category = new HabitCategory
        {
            Name = request.Name,
            Description = request.Description
        };
        
        await db.HabitCategories.AddAsync(category);
        await db.SaveChangesAsync();
        
        return Results.Created();
    }

    private static async Task<IResult> Update(int id, HabitCategoryRequest request, AppDbContext db)
    {
        var category = db.HabitCategories.FirstOrDefault(x => x.Id == id);

        if (category is null)
        {
            return Results.NotFound();
        }
        
        category.Name = request.Name;
        category.Description = request.Description;
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> Delete(int id, AppDbContext db)
    {
        var category = await db.HabitCategories.FindAsync(id);

        if (category is null)
        {
            return Results.NotFound();
        }
        
        db.HabitCategories.Remove(category);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}