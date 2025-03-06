using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class TodoRequestValidation : AbstractValidator<TodoRequest>
{
    public TodoRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}