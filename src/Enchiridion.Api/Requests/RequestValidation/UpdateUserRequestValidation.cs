using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class UpdateUserRequestValidation : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
    }
}