using FluentValidation;

// Validations for CreateDashboardCommand
namespace SensorFlow.Application.Dashboards.Commands
{
    public class CreateDashboardCommandValidator : AbstractValidator<CreateDashboardCommand>
    {
        public CreateDashboardCommandValidator() {
            // Using fluentvalidation, ensure that the name is between 3 and 50 chars in length
            RuleFor(x => x.name).MinimumLength(3).MaximumLength(50);
            // Ensure that the workspaceId is not empty
            RuleFor(x => x.workspaceId).NotEmpty();
        }
    }
}