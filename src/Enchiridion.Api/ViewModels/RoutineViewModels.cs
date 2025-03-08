using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;

public static class RoutineViewModels
{
    public record RoutineResponse(
        int Id,
        IEnumerable<RoutineStepViewModels.RoutineStepResponse> Steps);
    
    public static Expression<Func<Routine, RoutineResponse>> Projection =>
        routine => new RoutineResponse
        (
            routine.Id,
            routine.Steps.Select(s => RoutineStepViewModels.CreateProjection(s))
        );
}