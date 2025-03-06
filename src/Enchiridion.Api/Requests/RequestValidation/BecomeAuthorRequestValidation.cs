using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class BecomeAuthorRequestValidation : AbstractValidator<BecomeAuthorRequest>
{
    public BecomeAuthorRequestValidation()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Message).NotEmpty();
    }
}