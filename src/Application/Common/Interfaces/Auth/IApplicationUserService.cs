using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Common.Interfaces
{
    public interface IApplicationUserService
    {
        Task<User> GetUserByIdAsync(string userId);

        Task<string?> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(User user, string password, List<string> roles, bool isActive);

        Task<Result> DeleteUserAsync(string userId);
    }
}