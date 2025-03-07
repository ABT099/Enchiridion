namespace Enchiridion.Api.Requests;

public record TodoRequest(string Name, RepeatInterval? RepeatInterval);

public class TodoRequestValidation : AbstractValidator<TodoRequest>
{
    public TodoRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}