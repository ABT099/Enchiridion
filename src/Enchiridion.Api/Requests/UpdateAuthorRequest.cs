namespace Enchiridion.Api.Requests;

public record UpdateAuthorRequest(
    string Name,
    string? Description
);