using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.Commands
{
    public record DeactivateUserCommand(string userName) : IRequest<ErrorOr<User?>>;
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, ErrorOr<User?>>
    {
        private readonly IApplicationUserService _applicationUserService;

        public DeactivateUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<ErrorOr<User?>> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            return await _applicationUserService.DeactivateUserAsync(request.userName);
        }
    }
}
