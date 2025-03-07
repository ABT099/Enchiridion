namespace Enchiridion.Api.Requests;

public record TodoRequest(string Name, RepeatInterval? RepeatInterval);