using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Identity.Models;

namespace SensorFlow.Application.Identity.Commands
{
    public record LoginUserCommand(LoginRequestDTO loginRequestDTO) : IRequest<ErrorOr<LoginResponseDTO>>;
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ErrorOr<LoginResponseDTO>>
    {
        private readonly IUserAuthenticationService _userAuthententicationService;

        public LoginUserCommandHandler(IUserAuthenticationService userAuthententicationService)
        {
            _userAuthententicationService = userAuthententicationService;
        }

        public async Task<ErrorOr<LoginResponseDTO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userAuthententicationService.Login(request.loginRequestDTO);

            if (result.IsError)
                return result.Errors;

            return result.Value;
        }
    }
}
