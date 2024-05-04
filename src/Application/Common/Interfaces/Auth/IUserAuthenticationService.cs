using ErrorOr;
using SensorFlow.Application.Identity.Models;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */
namespace SensorFlow.Application.Common.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<ErrorOr<LoginResponseDTO>> Login(LoginRequestDTO request);
        Task<Error> Logout();
    }
}
