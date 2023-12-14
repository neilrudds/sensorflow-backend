using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;

namespace SensorFlow.Application.Common.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<Result> Login(LoginRequestDTO request);
        Task<Result> Logout();
    }
}
