using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;

public static class HabitCategoryViewModels
{
    public record HabitCategoryResponse(int Id, string Name, string Description);
    public record BasicHabitCategoryResponse(int Id, string Name);
    
    public static Expression<Func<HabitCategory, HabitCategoryResponse>> Projection => 
        category => new HabitCategoryResponse(category.Id, category.Name, category.Description);
    
    public static readonly Func<HabitCategory, BasicHabitCategoryResponse> CreateFlat = FlatProjection.Compile();
    
    private static Expression<Func<HabitCategory, BasicHabitCategoryResponse>> FlatProjection => 
        category => new BasicHabitCategoryResponse(category.Id, category.Name);
}