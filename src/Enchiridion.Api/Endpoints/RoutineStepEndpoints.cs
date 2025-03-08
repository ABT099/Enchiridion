using Enchiridion.Api.Services;

namespace Enchiridion.Api.Endpoints;

public static class RoutineStepEndpoints
{
    public static void AddRoutineStepsEndpoints(this RouteGroupBuilder api)
    {
        api.MapGet("routine-step/{routineId:int}", GetAllByRoutineId);
        api.MapPost("routine-step/{routineId:int}", Create);
        api.MapPut("routine-step/{id:int}", Update);
        api.MapDelete("routine-step/{id:int}", Delete);
        
        api.MapPatch("routine-step/{stepId:int}/habit/{habitId:int}", AddHabit);
        api.MapDelete("routine-step/{stepId:int}/habit/{habitId:int}", RemoveHabit);
        api.MapPatch("routine-step/{stepId:int}/habits", AddHabits);
        api.MapDelete("routine-step/{stepId:int}/habits", RemoveHabits);
        api.MapPatch("routine-step/{id:int}/todo", AddTodo);
        api.MapDelete("routine-step/{stepId:int}/todo/{todoId:int}", RemoveTodo);
        api.MapPatch("routine-step/{id:int}/order/{newOrder:int}", ChangeOrder);
    }

    private static async Task<IResult> GetAllByRoutineId(int routineId, AppDbContext db)
    {
        var steps = await db.RoutineSteps
            .AsNoTracking()
            .Where(s => s.RoutineId == routineId)
            .ToListAsync();
        
        return Results.Ok(steps);
    }
    
    private static async Task<IResult> Create(int routineId, RoutineStepRequest request, AppDbContext db)
    {
        var routineExists = await db.RoutineSteps
            .AsNoTracking()
            .AnyAsync(s => s.RoutineId == routineId);

        if (routineExists is false)
        {
            return Results.NotFound();
        }

        var step = new RoutineStep
        {
            Name = request.Name,
            Description = request.Description,
        };
        
        await db.RoutineSteps.AddAsync(step);
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }

    private static async Task<IResult> Update(int id, RoutineStepRequest request, AppDbContext db)
    {
        var step = await db.RoutineSteps.FirstOrDefaultAsync(x => x.Id == id);

        if (step is null)
        {
            return Results.NotFound();
        }
        
        step.Name = request.Name;
        step.Description = request.Description;
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> AddHabit(int stepId, int habitId, AppDbContext db)
    {
        var step = await db.RoutineSteps
            .Include(x => x.Habits)
            .FirstOrDefaultAsync(x => x.Id == stepId);

        if (step is null)
        {
            return Results.NotFound("step not found");
        }
        
        var habit = await db.Habits.FindAsync(habitId);

        if (habit is null)
        {
            return Results.NotFound("habit not found");
        }
        
        step.Habits.Add(habit);
        
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }
    
    private static async Task<IResult> RemoveHabit(int stepId, int habitId, AppDbContext db)
    {
        var step = await db.RoutineSteps
            .Include(x => x.Habits)
            .FirstOrDefaultAsync(x => x.Id == stepId);
        
        if (step is null)
        {
            return Results.NotFound("step not found");
        }
        
        var habit = await db.Habits.FindAsync(habitId);

        if (habit is null)
        {
            return Results.NotFound("habit not found");
        }
        
        step.Habits.Remove(habit);
        
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }
    
    private static async Task<IResult> AddHabits(int stepId, HashSet<int> habitId, AppDbContext db)
    {
        var step = await db.RoutineSteps
            .Include(x => x.Habits)
            .FirstOrDefaultAsync(x => x.Id == stepId);

        if (step is null)
        {
            return Results.NotFound("step not found");
        }
        
        var habits = await db.Habits
            .AsNoTracking()
            .Where(x => habitId.Contains(x.Id))
            .ToListAsync();
        
        step.Habits.AddRange(habits);
        
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }
    
    private static async Task<IResult> RemoveHabits(int stepId, HashSet<int> habitId, AppDbContext db)
    {
        var step = await db.RoutineSteps
            .Include(x => x.Habits)
            .FirstOrDefaultAsync(x => x.Id == stepId);
        
        if (step is null)
        {
            return Results.NotFound("step not found");
        }
        
        var habits = await db.Habits
            .AsNoTracking()
            .Where(x => habitId.Contains(x.Id))
            .ToListAsync();

        step.Habits.RemoveAll(habits.Contains);
        
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }

    private static async Task<IResult> AddTodo(int id, TodoRequest request, HttpContext context, AppDbContext db)
    {
        var userId = TokenService.GetUserId(context);

        var step = await db.RoutineSteps.FindAsync(id);

        if (step is null)
        {
            return Results.NotFound("step not found");
        }
        
        var todo = new Todo
        {
            UserId = userId,
            Name = request.Name,
            RoutineStep = step
        };

        await db.AddAsync(todo);
        await db.SaveChangesAsync();
        
        return Results.Ok();
    }

    private static async Task<IResult> RemoveTodo(int stepId, int todoId, AppDbContext db)
    {
        var step = await db.RoutineSteps
            .Include(x => x.Todos)
            .FirstOrDefaultAsync(x => x.Id == stepId);

        if (step is null)
        {
            return Results.NotFound("step not found");
        }

        var todo = step.Todos.FirstOrDefault(todo => todo.Id == todoId);

        if (todo is null)
        {
            return Results.NotFound("todo not found");
        }
        
        if (step.Todos.Contains(todo) == false)
        {
            return Results.BadRequest($"todo with id:{todoId} doesn't belong to step with id: {stepId}");
        }

        step.Todos.Remove(todo);
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    
    private static async Task<IResult> ChangeOrder(int id, int newOrder, AppDbContext db)
    {
        var step = await db.RoutineSteps.FirstOrDefaultAsync(x => x.Id == id);

        if (step is null)
        {
            return Results.NotFound();
        }

        step.StepOrder = newOrder;
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> Delete(int id, AppDbContext db)
    {
        var step = await db.RoutineSteps.FindAsync(id);

        if (step is null)
        {
            return Results.NotFound();
        }
        
        db.RoutineSteps.Remove(step);
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}