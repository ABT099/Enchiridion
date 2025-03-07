namespace Enchiridion.Api.Requests;

public record UpdateUserRequest(string FirstName, string LastName, string UserName);

public class UpdateUserRequestValidation : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
    }
}