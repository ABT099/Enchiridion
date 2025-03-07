namespace Enchiridion.Api.Requests;

public record CreateQuoteRequest(string QuoteText, int AuthorId);

public class CreateQuoteRequestValidation : AbstractValidator<CreateQuoteRequest>
{
    public CreateQuoteRequestValidation()
    {
        RuleFor(x => x.QuoteText).NotEmpty();
        RuleFor(x => x.AuthorId).NotEmpty();
    }
}