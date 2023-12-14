using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;

namespace SensorFlow.Application.Identity.Commands
{
    public record RemoveRolesCommand(string userName, List<string> roles) : IRequest<Result>;
    public class RemoveRolesCommandHandler : IRequestHandler<RemoveRolesCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public RemoveRolesCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<Result> Handle(RemoveRolesCommand request, CancellationToken cancellationToken)
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