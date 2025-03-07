namespace Enchiridion.Api.Requests;

public record UpdateQuoteRequest(string QuoteText);

public class UpdateQuoteRequestValidation : AbstractValidator<UpdateQuoteRequest>
{
    public UpdateQuoteRequestValidation()
    {
        RuleFor(x => x.QuoteText).NotEmpty();
    }
}