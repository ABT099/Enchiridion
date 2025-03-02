using Enchiridion.Models.Abstractions;

namespace Enchiridion.Models;

public class HabitCategory : ModelBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}