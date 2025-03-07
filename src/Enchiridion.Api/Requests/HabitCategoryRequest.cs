namespace Enchiridion.Api.Requests;

public record HabitCategoryRequest(string Name, string Description);

public class HabitCategoryRequestValidation : AbstractValidator<HabitCategoryRequest>
{
    public HabitCategoryRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}