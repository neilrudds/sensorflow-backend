using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Identity.Commands
{
    // Command
    public record DeleteUserCommand(string userId) : IRequest<Result>;

    // Command Handler
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public DeleteUserCommandHandler(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        { 
            return await _applicationUserService.DeleteUserAsync(request.userId);
        }
    }
}