namespace Enchiridion.Api.Requests;

public record HabitRequest(
    string Name,
    string? Description,
    int CategoryId);