using MediatR;
using SensorFlow.Application.Common.Interfaces;

namespace SensorFlow.Application.Identity.Commands
{
    // Command
    public record DeleteUserCommand(string userId) : IRequest;

    // Command Handler
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IApplicationUserService _applicationUserService;

        public DeleteUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _applicationUserService.DeleteUserAsync(request.userId);
        }
    }
}