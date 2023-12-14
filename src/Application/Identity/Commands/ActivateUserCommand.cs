using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.Commands
{
    // Command
    public record ActivateUserCommand(string userName) : IRequest<Result>;

    // Command Handler
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public ActivateUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<Result> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            return await _applicationUserService.ActivateUserAsync(request.userName);
        }
    }
}