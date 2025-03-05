using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;
    
public static class QuoteViewModels
{
    public record QuoteResponse(int Id, string QuoteText, AuthorViewModels.BasicAuthorResponse Author);

    public static Expression<Func<Quote, QuoteResponse>> Projection =>
        quote => new QuoteResponse
        (
            quote.Id,
            quote.QuoteText,
            AuthorViewModels.CreateFlat(quote.Author)
        );
}