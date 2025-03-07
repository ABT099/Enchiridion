using Enchiridion.Models.Abstractions;

namespace Enchiridion.Models;

public class Todo : ModelBase
{
    public required int UserId { get; set; }
    public RoutineStep? RoutineStep { get; set; }
    public required string Name { get; set; }
    public bool IsComplete { get; set; } = false;
    public bool isRepeated { get; set; } = false;
    public RepeatOptions? TodoOptions { get; set; }
}