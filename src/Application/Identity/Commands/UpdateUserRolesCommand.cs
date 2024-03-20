using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.Commands
{
    public record UpdateUserRolesCommand(string userName, List<string> roles) : IRequest<ErrorOr<User?>>;
    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand, ErrorOr<User?>>
    {
        private readonly IApplicationUserService _applicationUserService;

        public UpdateUserRolesCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<ErrorOr<User?>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var updateRoles = new RolesRequestDTO
            {
                userName = request.userName,
                roles = request.roles
            };

            return await _applicationUserService.UpdateUserRolesAsync(updateRoles);
        }
    }
}
