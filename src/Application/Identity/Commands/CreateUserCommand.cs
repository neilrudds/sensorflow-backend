using MediatR;
using ErrorOr;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.Commands
{
    // Command
    public record CreateUserCommand(string userName, string firstName, string lastName, string email, string password, string tenantId, List<string> roles) : IRequest<ErrorOr<User?>>;

    // Command Handler
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<User?>>
    {
        private readonly IApplicationUserService _applicationUserService;

        public CreateUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<ErrorOr<User?>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User(
                request.userName,
                request.firstName,
                request.lastName,
                request.email,
                request.tenantId);

            var user = await _applicationUserService.GetUserByUserNameAsync(request.userName);

            if (user != null)
            {
                Error.Failure(description: "Username already exists");
            }

            return await _applicationUserService.CreateUserAsync(newUser, request.password, request.roles, true);
        }
    }
}