using FluentValidation;

namespace Enchiridion.Api.Requests.RequestValidation;

public class UpdateQuoteRequestValidation : AbstractValidator<UpdateQuoteRequest>
{
    public UpdateQuoteRequestValidation()
    {
        RuleFor(x => x.QuoteText).NotEmpty();
    }
}