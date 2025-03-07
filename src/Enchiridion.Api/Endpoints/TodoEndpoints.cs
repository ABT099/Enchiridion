using Enchiridion.Api.Services;
using Enchiridion.Api.ViewModels;

namespace Enchiridion.Api.Endpoints;

public static class TodoEndpoints
{
    public static void AddTodoEndpoints(this RouteGroupBuilder api)
    {
        api.MapGet("todos", GetTodos);
        api.MapGet("todos/{id:int}", GetById);
        api.MapPost("todos", Create);
        api.MapPut("todos", Update);
        api.MapPatch("todos/{id:int}", Complete);
        api.MapDelete("todos", Delete);
    }

    private static async Task<IResult> GetTodos(HttpContext context, AppDbContext db)
    {
        var userId = TokenService.GetUserId(context);
        
        var todos = await db.Todos
            .Where(x => x.UserId == userId && x.RoutineStep == null)
            .Select(TodoViewModels.Projection)
            .ToListAsync();

        return Results.Ok(todos);
    }

    private static async Task<IResult> GetById(int id, HttpContext context, AppDbContext db)
    {
        var userId = TokenService.GetUserId(context);
        
        var todo = await db.Todos
            .Where(x => x.Id == id && x.UserId == userId)
            .FirstOrDefaultAsync();

        return todo is null
            ? Results.NotFound()
            : Results.Ok(todo);
    }

    private static async Task<IResult> Create(TodoRequest request, HttpContext context, AppDbContext db)
    {
        var userId = TokenService.GetUserId(context);

        Todo todo;
        
        if (request.RepeatInterval is not null)
        {
            todo = new Todo
            {
                UserId = userId,
                Name = request.Name,
                TodoOptions = new RepeatOptions
                {
                    RepeatInterval = request.RepeatInterval.Value,
                }
            };
        }
        else
        {
            todo = new Todo
            {
                UserId = userId,
                Name = request.Name
            };
        }

        await db.Todos.AddAsync(todo);
        await db.SaveChangesAsync();

        return Results.Created();
    }

    private static async Task<IResult> Update(int id, TodoRequest request, HttpContext context, AppDbContext db)
    {
        var userId = TokenService.GetUserId(context);

        var todo = await GetTodoEntity(id, userId, db);

        if (todo is null)
        {
            return Results.NotFound();
        }

        todo.Name = request.Name;

        if (request.RepeatInterval is not null)
        {
            if (todo.TodoOptions is null)
            {
                todo.TodoOptions = new RepeatOptions
                {
                    RepeatInterval = request.RepeatInterval.Value,
                };
            }
            else
            {
                todo.TodoOptions.RepeatInterval = request.RepeatInterval.Value;
            }
        }
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> Complete(int id, HttpContext context, AppDbContext db)
    {
        var userId = TokenService.GetUserId(context);

        var todo = await GetTodoEntity(id, userId, db);

        if (todo is null)
        {
            return Results.NotFound();
        }

        todo.IsComplete = true;
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private static async Task<IResult> Delete(int id, HttpContext context, AppDbContext db)
    {
        var userId = TokenService.GetUserId(context);
        
        var todo = await GetTodoEntity(id, userId, db);

        if (todo is null)
        {
            return Results.NotFound();
        }

        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    
    private static async Task<Todo?> GetTodoEntity(int id, int userId, AppDbContext db) =>
        await db.Todos
            .Where(x => x.Id == id && x.UserId == userId)
            .FirstOrDefaultAsync();
}