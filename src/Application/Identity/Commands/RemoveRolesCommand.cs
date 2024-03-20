using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.Commands
{
    public record RemoveRolesCommand(string userName, List<string> roles) : IRequest<ErrorOr<User?>>;
    public class RemoveRolesCommandHandler : IRequestHandler<RemoveRolesCommand, ErrorOr<User?>>
    {
        private readonly IApplicationUserService _applicationUserService;

        public RemoveRolesCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<ErrorOr<User?>> Handle(RemoveRolesCommand request, CancellationToken cancellationToken)
        {
            var removeRoles = new RolesRequestDTO
            {
                userName = request.userName,
                roles = request.roles
            };

            return await _applicationUserService.RemoveRolesFromUserAsync(removeRoles);
        }
    }
}