namespace Enchiridion.Api.Requests;

public record AuthorRequest(
        string Name,
        string? Description
);