using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;

namespace SensorFlow.Application.Identity.Commands
{
    public record AddRolesCommand(string userName, List<string> roles) : IRequest<Result>;

    public class AddRolesCommandHandler : IRequestHandler<AddRolesCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public AddRolesCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<Result> Handle(AddRolesCommand request, CancellationToken cancellationToken)
        {
            var addRoles = new RolesRequestDTO
            {
                userName = request.userName,
                roles = request.roles
            };
            
            return await _applicationUserService.AddRolesToUserAsync(addRoles);
        }
    }
}
