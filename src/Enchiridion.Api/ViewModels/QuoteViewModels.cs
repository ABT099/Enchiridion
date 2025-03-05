using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;
    
public static class QuoteViewModels
{
    public static Expression<Func<Quote, object>> Projection =>
        quote => new
        {
            quote.Id,
            quote.QuoteText,
            Author = AuthorViewModels.CreateFlat(quote.Author)
        };
}