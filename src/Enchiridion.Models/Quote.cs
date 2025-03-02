using Enchiridion.Models.Abstractions;

namespace Enchiridion.Models;

public class Quote : ModelBase
{
    public required string QuoteText { get; set; }
    public required Author Author { get; set; }
}