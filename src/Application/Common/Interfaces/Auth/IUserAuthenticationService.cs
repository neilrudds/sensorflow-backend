using ErrorOr;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;

namespace SensorFlow.Application.Common.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<ErrorOr<LoginResponseDTO>> Login(LoginRequestDTO request);
        Task<Error> Logout();
    }
}
