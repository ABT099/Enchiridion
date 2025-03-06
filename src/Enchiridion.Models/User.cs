using Enchiridion.Models.Abstractions;

namespace Enchiridion.Models;

public class User : ModelBase
{
    public required string AuthId { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; init; }
    public required string UserName { get; set; }
    public Author? Author { get; set; }
    public List<Routine> Routines { get; set; } = [];
    public List<Habit> Habits { get; set; } = [];
    public List<Todo> Todos { get; set; } = [];
    public List<Author> SubscribedAuthors { get; set; } = [];
    public List<AuthorRequest> AuthorRequests { get; set; } = [];
}