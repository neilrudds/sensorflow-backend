using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;

namespace SensorFlow.Application.Identity.Commands
{
    public record LoginUserCommand(LoginRequestDTO loginRequestDTO) : IRequest<(Result result, LoginResponseDTO? response)>;
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, (Result result, LoginResponseDTO? response)>
    {
        private readonly IUserAuthenticationService _userAuthententicationService;

        public LoginUserCommandHandler(IUserAuthenticationService userAuthententicationService)
        {
            _userAuthententicationService = userAuthententicationService;
        }

        public async Task<(Result result, LoginResponseDTO? response)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _userAuthententicationService.Login(request.loginRequestDTO);
        }
    }
}
