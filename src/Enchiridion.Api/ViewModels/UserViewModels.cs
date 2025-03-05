using System.Linq.Expressions;

namespace Enchiridion.Api.ViewModels;

public static class UserViewModels
{ 
    public record BasicUserResponse(int Id, string FirstName, string LastName, string Email, string Username);
    public record UserResponse
    (
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string Username,
        Routine? TodayRoutine
    );
    
    public static Expression<Func<User, BasicUserResponse>> FlatProjection =>
        user => new BasicUserResponse
        (
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.UserName
        );

    public static Expression<Func<User, UserResponse>> Projection =>
        user => new UserResponse
        (
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.UserName,
            user.Routines.FirstOrDefault(routine => routine.Days.Equals(DateTime.Today.DayOfWeek))
        );
}

