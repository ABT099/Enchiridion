namespace Enchiridion.Api.Requests;

public record UpdateRoutineRequest(List<DayOfWeek> AddedDays, List<DayOfWeek> RemovedDays);

public class UpdateRoutineRequestValidation : AbstractValidator<UpdateRoutineRequest>
{
    public UpdateRoutineRequestValidation()
    {
        RuleFor(x => x.AddedDays)
            .NotNull().WithMessage("AddedDays cannot be null.")
            .Must(list => list.Distinct().Count() == list.Count)
            .WithMessage("AddedDays must not contain duplicate days.");

        RuleFor(x => x.RemovedDays)
            .NotNull().WithMessage("RemovedDays cannot be null.")
            .Must(list => list.Distinct().Count() == list.Count)
            .WithMessage("RemovedDays must not contain duplicate days.");

        RuleFor(x => x)
            .Must(request => !request.AddedDays.Intersect(request.RemovedDays).Any())
            .WithMessage("AddedDays and RemovedDays cannot contain the same day.");
    }
}