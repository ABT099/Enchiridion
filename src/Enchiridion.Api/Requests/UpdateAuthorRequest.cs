namespace Enchiridion.Api.Requests;

public record UpdateAuthorRequest(
    string Name,
    string? Description
);

public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
{
    public UpdateAuthorRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}