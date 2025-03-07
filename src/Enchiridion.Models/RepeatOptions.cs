namespace Enchiridion.Models;

public class RepeatOptions
{
    public RepeatOptions()
    {
        TargetDate = RepeatInterval switch
        {
            RepeatInterval.Daily => DateTime.UtcNow.AddDays(1),
            RepeatInterval.Weekly => DateTime.UtcNow.AddDays(7),
            RepeatInterval.Monthly => DateTime.UtcNow.AddMonths(1),
            RepeatInterval.Yearly => DateTime.UtcNow.AddYears(1),
            _ => throw new Exception("Invalid repeat interval")
        };
    }
    
    public required RepeatInterval RepeatInterval { get; set; }
    public DateTime TargetDate { get; set; }
}

public enum RepeatInterval
{
    Daily,
    Weekly,
    Monthly,
    Yearly
}