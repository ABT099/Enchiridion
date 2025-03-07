namespace Enchiridion.Api.Requests;

public record BecomeAuthorRequest(int UserId, string Message);

public class BecomeAuthorRequestValidation : AbstractValidator<BecomeAuthorRequest>
{
    public BecomeAuthorRequestValidation()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Message).NotEmpty();
    }
}