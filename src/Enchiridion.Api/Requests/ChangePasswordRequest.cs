namespace Enchiridion.Api.Requests;

public record ChangePasswordRequest(string OldPassword, string NewPassword);