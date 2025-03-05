using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;

public static class AuthorViewModels
{
    public record BasicAuthorResponse(int Id, string Name);
    public record AuthorResponse(int Id, string Name, IEnumerable<AuthorQuote> Quotes);
    public record AuthorQuote(int Id, string Title);
    
    public static readonly Func<Author, BasicAuthorResponse> CreateFlat = FlatProjection.Compile();

    public static Expression<Func<Author, BasicAuthorResponse>> FlatProjection =>
        author => new BasicAuthorResponse
        (
            author.Id,
            author.Name
        );

    public static Expression<Func<Author, AuthorResponse>> Projection =>
        author => new AuthorResponse
        (
            author.Id,
            author.Name,
            author.Quotes.Select(x => new AuthorQuote
            (
                x.Id,
                x.QuoteText.Substring(0, 20)
            ))
        );
}