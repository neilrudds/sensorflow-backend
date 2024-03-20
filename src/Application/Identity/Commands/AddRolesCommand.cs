using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.Commands
{
    public record AddRolesCommand(string userName, List<string> roles) : IRequest<ErrorOr<User?>>;

    public class AddRolesCommandHandler : IRequestHandler<AddRolesCommand, ErrorOr<User?>>
    {
        private readonly IApplicationUserService _applicationUserService;

        public AddRolesCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<ErrorOr<User?>> Handle(AddRolesCommand request, CancellationToken cancellationToken)
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
