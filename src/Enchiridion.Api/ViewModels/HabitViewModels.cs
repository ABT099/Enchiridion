using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;

public static class HabitViewModels
{
    public record BasicHabitResponse(
        int Id,
        string Name,
        HabitCategoryViewModels.BasicHabitCategoryResponse Category,
        string Status);

    public record HabitResponse(
        int Id,
        string Name,
        string? Description,
        HabitCategoryViewModels.BasicHabitCategoryResponse Category,
        string Status);

    public static Expression<Func<Habit, BasicHabitResponse>> FlatProjection =>
        habit => new BasicHabitResponse
        (
            habit.Id,
            habit.Name,
            HabitCategoryViewModels.CreateFlat(habit.HabitCategory),
            habit.Status.ToString()
        );

    public static Expression<Func<Habit, HabitResponse>> Projection =>
        habit => new HabitResponse
        (
            habit.Id,
            habit.Name,
            habit.Description,
            HabitCategoryViewModels.CreateFlat(habit.HabitCategory),
            habit.Status.ToString()
        );
}