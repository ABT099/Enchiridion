using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;

public static class TodoViewModels
{
    public record TodoResponse(int Id, int? RoutineId, string Name, bool IsComplete);

    public static Expression<Func<Todo, TodoResponse>> Projection =>
        todo => new TodoResponse
        (
            todo.Id,
            todo.RoutineStep == null ? null : todo.RoutineStep.Id,
            todo.Name,
            todo.IsComplete
        );
}