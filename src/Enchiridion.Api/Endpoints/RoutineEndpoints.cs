using Enchiridion.Api.Services;

namespace Enchiridion.Api.Endpoints;

public static class RoutineEndpoints
{
    public static void AddRoutineEndpoints(this RouteGroupBuilder api)
    {
        api.MapGet("routines/day/{day}", GetByDay);
        api.MapPost("routines", Create);
        api.MapPut("routines/{id:int}", Update);
        api.MapDelete("routines/{id:int}", Delete);
    }

    private static async Task<IResult> GetByDay(AppDbContext db, HttpContext httpContext, DayOfWeek? day)
    {
        var userId = TokenService.GetUserId(httpContext);

        day ??= DateTime.Today.DayOfWeek;
        
        var routines = await db.Routines
            .AsNoTracking()
            .Include(x => x.Steps)
            .Where(x => x.UserId == userId && x.Days.Contains(day.Value))
            .ToListAsync();
        
        return Results.Ok(routines);
    }

    private static async Task<IResult> Create(AppDbContext db, HttpContext httpContext, CreateRoutineRequest request)
    {
        var userId = TokenService.GetUserId(httpContext);
        
        var conflictDay = await db.Routines
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .SelectMany(r => r.Days)
            .FirstOrDefaultAsync(day => request.Days.Contains(day));

        if (!EqualityComparer<DayOfWeek>.Default.Equals(conflictDay, default))
        {
            return Results.BadRequest($"An existing routine already uses the same day: {conflictDay}");
        }
        
        var routine = new Routine
        {
            UserId = userId,
            Days = request.Days,
            Steps = request.Steps
                .Select((step, index) => new RoutineStep
                {
                    Name = step.Name,
                    Description = step.Description,
                    StepOrder = index
                })
                .ToList()
        };
        
        await db.Routines.AddAsync(routine);
        await db.SaveChangesAsync();
        
        return Results.Created($"/routines/{routine.Id}", routine);
    }

    private static async Task<IResult> Update(AppDbContext db, HttpContext httpContext, int id, UpdateRoutineRequest request)
    {
        var userId = TokenService.GetUserId(httpContext);
        
        var conflictDay = await db.Routines
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .SelectMany(r => r.Days)
            .FirstOrDefaultAsync(day => request.AddedDays.Contains(day));

        if (!EqualityComparer<DayOfWeek>.Default.Equals(conflictDay, default))
        {
            return Results.BadRequest($"An existing routine already uses the same day: {conflictDay}");
        }
        
        var routine = await db.Routines
            .Where(x => x.UserId == userId && x.Id == id)
            .FirstOrDefaultAsync();

        if (routine == null)
        {
            return Results.NotFound();
        }
        
        routine.Days.RemoveAll(request.RemovedDays.Contains);
        routine.Days.AddRange(request.AddedDays);
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> Delete(AppDbContext db, HttpContext httpContext, int id)
    {
        var userId = TokenService.GetUserId(httpContext);
        
        var routine = await db.Routines
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.Id == id)
            .FirstOrDefaultAsync();

        if (routine == null)
        {
            return Results.NotFound();
        }
        
        db.Routines.Remove(routine);
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }
}   