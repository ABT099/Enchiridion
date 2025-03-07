namespace Enchiridion.Api.Requests;

public record AuthorRequest(
        string Name,
        string? Description
);

public class AuthorRequestValidation : AbstractValidator<AuthorRequest>
{
        public AuthorRequestValidation()
        {
                RuleFor(x => x.Name).NotEmpty();
        }
}