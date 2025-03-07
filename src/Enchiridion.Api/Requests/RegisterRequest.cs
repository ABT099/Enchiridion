namespace Enchiridion.Api.Requests;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password);
    
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