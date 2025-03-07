using Enchiridion.Api.Services;
using Enchiridion.Api.ViewModels;

namespace Enchiridion.Api.Endpoints;

public static class HabitEndpoints
{
    public static void AddHabitEndpoints(this RouteGroupBuilder api)
    {
        api.MapGet("habits", GetMyHabits);
        api.MapGet("habits/{id:int}", GetById);
        api.MapPost("habits", Create);
        api.MapPut("habits/{id:int}", Update);
        api.MapPatch("habits/{id:int}", UpdateStatus);
        api.MapDelete("habits/{id:int}", Delete);
    }

    private static async Task<IResult> GetMyHabits(HttpContext httpContext, AppDbContext db)
    {
        var userId = TokenService.GetUserId(httpContext);
        
        var habits = await db.Habits
            .Where(x => x.UserId == userId)
            .Select(HabitViewModels.FlatProjection)
            .ToListAsync();
        
        return Results.Ok(habits);
    }

    private static async Task<IResult> GetById(int id, HttpContext httpContext, AppDbContext db)
    {
        var userId = TokenService.GetUserId(httpContext);
        
        var habit = await db.Habits
            .Where(x => x.Id == id && x.UserId == userId)
            .Select(HabitViewModels.Projection)
            .FirstOrDefaultAsync();
        
        return habit is null 
            ? Results.NotFound() 
            : Results.Ok(habit);
    }
    
    private static async Task<IResult> Create(HabitRequest request, HttpContext httpContext, AppDbContext db)
    {
        var userId = TokenService.GetUserId(httpContext);
        
        if (!await UserExists(userId, db))
        {
            return Results.NotFound("User not found");
        }
        
        var category = await GetCategory(request.CategoryId, db);

        if (category is null)
        {
            return Results.NotFound("Category not found");
        }

        var habit = new Habit
        {
            UserId = userId,
            Name = request.Name,
            Description = request.Description,
            HabitCategory = category,
            HabitOptions = new RepeatOptions
            {
                RepeatInterval = request.RepeatInterval
            }
        };
        
        await db.Habits.AddAsync(habit);
        await db.SaveChangesAsync();
        
        return Results.Created();
    }

    private static async Task<IResult> Update(int id, HabitRequest request, HttpContext httpContext, AppDbContext db)
    {
        var userId = TokenService.GetUserId(httpContext);
        var habit = await GetHabitEntity(id, userId, db);
        
        if (habit is null)
        {
            return Results.NotFound("Habit not found");
        }
        
        var category = await GetCategory(request.CategoryId, db);

        if (category is null)
        {
            return Results.NotFound("Category not found");
        }
        
        habit.Name = request.Name;
        habit.Description = request.Description;
        habit.HabitCategory = category;
        habit.HabitOptions.RepeatInterval = request.RepeatInterval;
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> UpdateStatus(int id, HabitStatus status, AppDbContext db, HttpContext httpContext)
    {
        var userId = TokenService.GetUserId(httpContext);
        
        var habit = await GetHabitEntity(id, userId, db);
        
        if (habit is null)
        {
            return Results.NotFound("Habit not found");
        }
        
        habit.Status = status;
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> Delete(int id, HttpContext httpContext, AppDbContext db)
    {
        var userId = TokenService.GetUserId(httpContext);
        
        var habit = await GetHabitEntity(id, userId, db);
        
        if (habit is null)
        {
            return Results.NotFound("Habit not found");
        }
        
        db.Habits.Remove(habit);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    
    #region Helpers
    private static async Task<bool> UserExists(int userId, AppDbContext db)
        => await db.Users.AnyAsync(u => u.Id == userId);

    private static async Task<Habit?> GetHabitEntity(int id, int userId, AppDbContext db)
        => await db.Habits
            .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

    private static async Task<HabitCategory?> GetCategory(int categoryId, AppDbContext db)
        => await db.HabitCategories.FindAsync(categoryId);
    #endregion
}