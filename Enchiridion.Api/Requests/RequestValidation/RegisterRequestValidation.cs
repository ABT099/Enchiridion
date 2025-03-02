using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class RegisterRequestValidation : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidation()
    {
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}