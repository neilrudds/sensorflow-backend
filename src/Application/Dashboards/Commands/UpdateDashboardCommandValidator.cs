using FluentValidation;

// Validations for UpdateDashboardCommand
namespace SensorFlow.Application.Dashboards.Commands
{
    public class UpdateDashboardCommandValidator : AbstractValidator<UpdateDashboardCommand>
    {
        public UpdateDashboardCommandValidator() {
            RuleFor(x => x.dashboardId).NotEmpty();
            RuleFor(x => x.gridWidgets).MaximumLength(10000);
            RuleFor(x => x.gridLayout).MaximumLength(10000);
        }
    }
}