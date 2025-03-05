using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class AuthorRequestValidation : AbstractValidator<AuthorRequest>
{
    public AuthorRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}