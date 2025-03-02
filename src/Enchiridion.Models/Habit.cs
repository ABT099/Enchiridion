using Enchiridion.Models.Abstractions;

namespace Enchiridion.Models;

public class Habit : ModelBase
{
    public required int UserId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required HabitCategory HabitCategory { get; set; }
    public HabitStatus Status { get; set; } = HabitStatus.Active;
}

public enum HabitStatus
{
    Active,
    Inactive,
    Completed
}