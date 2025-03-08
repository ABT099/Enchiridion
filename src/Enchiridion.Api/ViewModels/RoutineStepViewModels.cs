using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;

public static class RoutineStepViewModels
{
    public record RoutineStepResponse(
        int Id,
        string Name,
        string? Description,
        bool IsCompleted,
        IEnumerable<HabitViewModels.BasicHabitResponse> Habits,
        IEnumerable<TodoViewModels.TodoResponse> Todos);
    
    public static readonly Func<RoutineStep, RoutineStepResponse> CreateProjection = Projection.Compile(); 
    
    public static Expression<Func<RoutineStep, RoutineStepResponse>> Projection =>
        step => new RoutineStepResponse
        (
            step.Id,
            step.Name,
            step.Description,
            step.IsCompleted,
            step.Habits.Select(h => HabitViewModels.CreateFlat(h)),
            step.Todos.Select(t => TodoViewModels.CreateProjection(t))
        );
}