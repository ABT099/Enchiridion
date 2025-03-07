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

public record RoutineStepRequest(
    string Name,
    string? Description);

public class RoutineStepRequestValidation : AbstractValidator<RoutineStepRequest>
{
    public RoutineStepRequestValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Step name is required.")
            .MaximumLength(100).WithMessage("Step name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}