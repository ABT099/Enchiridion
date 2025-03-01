using Enchiridion.Models.Abstractions;

namespace Enchiridion.Models;

public class Routine : ModelBase
{
    public required int UserId { get; set; }
    public List<RoutineStep> Steps { get; set; } = [];
    public List<DayOfWeek> Days { get; set; } = [];
}