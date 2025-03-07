namespace Enchiridion.Api.Requests;

public record ChangePasswordRequest(string OldPassword, string NewPassword);

public class ChangePasswordRequestValidation : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidation()
    {
        RuleFor(x => x.OldPassword).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty();
    }
}