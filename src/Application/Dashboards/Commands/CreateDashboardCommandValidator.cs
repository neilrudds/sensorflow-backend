using FluentValidation;

namespace SensorFlow.Application.Dashboards.Commands
{
    public class CreateDashboardCommandValidator : AbstractValidator<CreateDashboardCommand>
    {
        public CreateDashboardCommandValidator() {
            RuleFor(x => x.name).MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.workspaceId).NotEmpty();
        }
    }
}