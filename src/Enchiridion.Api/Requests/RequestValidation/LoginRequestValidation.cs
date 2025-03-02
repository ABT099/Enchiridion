using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class LoginRequestValidation : AbstractValidator<LoginRequest>
{
    public LoginRequestValidation()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}