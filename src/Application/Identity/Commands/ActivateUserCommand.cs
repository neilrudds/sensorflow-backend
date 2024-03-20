using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.Commands
{
    // Command
    public record ActivateUserCommand(string userName) : IRequest<ErrorOr<User>>;

    // Command Handler
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, ErrorOr<User?>>
    {
        private readonly IApplicationUserService _applicationUserService;

        public ActivateUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<ErrorOr<User?>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _applicationUserService.GetUserByUserNameAsync(request.userName);

            if (user is null)
            {
                return Error.NotFound(description: "User not found");
            }

            return await _applicationUserService.ActivateUserAsync(request.userName);
        }
    }
}