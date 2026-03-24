using FluentValidation;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Validators;

public class TaskItemValidator : AbstractValidator<TaskItem>
{
    public TaskItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Deadline must be in the future");

        RuleFor(x => x.Status)
            .Must(status => new[] { "Todo", "InProgress", "Done" }.Contains(status))
            .WithMessage("Invalid status");
    }
}