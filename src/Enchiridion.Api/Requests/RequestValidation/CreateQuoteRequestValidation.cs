using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class CreateQuoteRequestValidation : AbstractValidator<CreateQuoteRequest>
{
    public CreateQuoteRequestValidation()
    {
        RuleFor(x => x.QuoteText).NotEmpty();
        RuleFor(x => x.AuthorId).NotEmpty();
    }
}