using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;

public static class AuthorViewModels
{
    public static readonly Func<Author, object> CreateFlat = FlatProjection.Compile();
        
    public static Expression<Func<Author, object>> FlatProjection => 
        author => new
        {
            author.Id,
            author.Name,
            author.AvatarUrl
        };

    public static Expression<Func<Author, object>> Projection =>
        author => new
        {
            author.Id,
            author.Name,
            author.AvatarUrl,
            Quotes = author.Quotes.Select(x => new
            {
                x.Id,
                Title = x.QuoteText.Substring(0, 20)
            })
        };
}