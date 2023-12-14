using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;

namespace SensorFlow.Application.Identity.Commands
{
    public record UpdateUserRolesCommand(string userName, List<string> roles) : IRequest<Result>;
    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public UpdateUserRolesCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<Result> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
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
