using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Identity.Commands
{
    public record ChangeUserPasswordCommand(string userId, string oldPassword, string newPassword) : IRequest<Result>;
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public ChangeUserPasswordCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<Result> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _applicationUserService.ChangePasswordAsync(request.userId, request.oldPassword, request.newPassword);
        }
    }
}
