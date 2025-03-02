namespace Enchiridion.Api.Requests;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password);