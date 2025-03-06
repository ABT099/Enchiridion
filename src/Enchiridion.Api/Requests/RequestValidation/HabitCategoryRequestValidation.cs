using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class HabitCategoryRequestValidation : AbstractValidator<HabitCategoryRequest>
{
    public HabitCategoryRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}