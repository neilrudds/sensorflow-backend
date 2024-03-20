using FluentValidation;

namespace SensorFlow.Application.Gateways.Commands
{
    public class DeleteGatewayValidator : AbstractValidator<DeleteGatewayCommand>
    {
        public DeleteGatewayValidator() {
            RuleFor(x => x.id).NotEmpty();
        }
    }
}