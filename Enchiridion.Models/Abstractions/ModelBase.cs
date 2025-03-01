namespace Enchiridion.Models.Abstractions;

public abstract class ModelBase
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; set; }
}