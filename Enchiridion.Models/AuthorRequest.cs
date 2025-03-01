using Enchiridion.Models.Abstractions;

namespace Enchiridion.Models;

public class AuthorRequest : ModelBase
{
    public required int UserId { get; set; }
    public required string Message { get; set; }
    private RequestStatus Status { get; set; } = RequestStatus.Pending;
}

public enum RequestStatus
{
    Pending,
    Approved,
    Rejected
}