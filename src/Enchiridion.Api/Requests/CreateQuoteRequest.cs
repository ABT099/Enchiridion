namespace Enchiridion.Api.Requests;

public record CreateQuoteRequest(string QuoteText, int AuthorId);