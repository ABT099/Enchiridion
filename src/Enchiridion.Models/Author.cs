namespace Enchiridion.Models;

public class Author
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }
    public List<Quote> Quotes { get; set; } = [];
}