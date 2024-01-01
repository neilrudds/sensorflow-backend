using MediatR;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SensorFlow.Application.Identity.Commands
{
    // Command
    public record CreateUserCommand(string userName, string firstName, string lastName, string email, string password, string tenantId, List<string> roles) : IRequest<(Result result, string UserId)>;

    // Command Handler
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, (Result result, string UserId)>
    {
        private readonly IApplicationUserService _applicationUserService;

        public CreateUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<(Result result, string UserId)> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.userName, request.firstName, request.lastName, request.email, request.tenantId);
            return await _applicationUserService.CreateUserAsync(user, request.password, request.roles, true);
        }
    }
}