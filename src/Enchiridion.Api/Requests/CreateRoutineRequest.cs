namespace Enchiridion.Api.Requests;

public record CreateRoutineRequest(
    List<RoutineStepRequest> Steps,
    List<DayOfWeek> Days);
    
public class CreateRoutineRequestValidation : AbstractValidator<CreateRoutineRequest>
{
    public CreateRoutineRequestValidation()
    {
        RuleFor(x => x.Steps)
            .NotNull().WithMessage("Steps cannot be null.")
            .NotEmpty().WithMessage("At least one step is required.");
        
        RuleForEach(x => x.Steps)
            .SetValidator(new RoutineStepRequestValidation());

        RuleFor(x => x.Days)
            .NotNull().WithMessage("Days cannot be null.")
            .NotEmpty().WithMessage("At least one day is required.")
            .Must(days => days.Distinct().Count() == days.Count)
            .WithMessage("Days list cannot contain duplicate days.");
    }
}
