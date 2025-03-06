using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class ChangePasswordRequestValidation : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidation()
    {
        RuleFor(x => x.OldPassword).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty();
    }
}