namespace Enchiridion.Api.Requests;

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