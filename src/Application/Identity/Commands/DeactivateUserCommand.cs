using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Identity.Commands
{
    public record DeactivateUserCommand(string userName) : IRequest<Result>;
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public DeactivateUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<Result> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            return await _applicationUserService.DeactivateUserAsync(request.userName);
        }
    }
}
