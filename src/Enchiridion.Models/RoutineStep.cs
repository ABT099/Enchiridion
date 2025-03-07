using Enchiridion.Models.Abstractions;

namespace Enchiridion.Models;

public class RoutineStep : ModelBase
{
    public int RoutineId { get; set; }
    public List<Habit> Habits { get; set; } = [];
    public List<Todo> Todos { get; set; } = [];
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int StepOrder { get; set; }
    public bool IsCompleted { get; set; } = false;
}