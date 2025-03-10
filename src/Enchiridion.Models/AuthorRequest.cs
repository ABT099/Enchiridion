using Enchiridion.Models.Abstractions;

namespace Enchiridion.Models;

public class AuthorRequest : ModelBase
{
    public required int UserId { get; init; }
    public required string Message { get; init; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
}

public enum RequestStatus
{
    Pending,
    Approved,
    Rejected
}