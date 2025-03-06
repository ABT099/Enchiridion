using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class HabitRequestValidation : AbstractValidator<HabitRequest>
{
    public HabitRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}