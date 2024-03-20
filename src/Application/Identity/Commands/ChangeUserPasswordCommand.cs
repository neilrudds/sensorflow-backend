using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.Commands
{
    public record ChangeUserPasswordCommand(string userId, string oldPassword, string newPassword) : IRequest<ErrorOr<User?>>;
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, ErrorOr<User?>>
    {
        private readonly IApplicationUserService _applicationUserService;

        public ChangeUserPasswordCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<ErrorOr<User?>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _applicationUserService.ChangePasswordAsync(request.userId, request.oldPassword, request.newPassword);
        }
    }
}
