using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;

public static class TodoViewModels
{
    public record TodoResponse(int Id, string Name, bool IsComplete);

    public static readonly Func<Todo, TodoResponse> CreateProjection = Projection.Compile();
    
    public static Expression<Func<Todo, TodoResponse>> Projection =>
        todo => new TodoResponse
        (
            todo.Id,
            todo.Name,
            todo.IsComplete
        );
}