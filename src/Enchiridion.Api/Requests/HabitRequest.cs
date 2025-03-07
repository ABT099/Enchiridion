namespace Enchiridion.Api.Requests;

public record HabitRequest(
    string Name,
    string? Description,
    int CategoryId,
    RepeatInterval RepeatInterval);
    
public class HabitRequestValidation : AbstractValidator<HabitRequest>
{
    public HabitRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}