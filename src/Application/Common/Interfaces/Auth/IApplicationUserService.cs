using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Common.Interfaces
{
    public interface IApplicationUserService
    {
        Task<User> GetUserByIdAsync(string userId);

        Task<(Result result, string? userName)> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result result, string UserId)> CreateUserAsync(User user, string password, List<string> roles, bool isActive);

        Task<Result> ActivateUserAsync(string username);

        Task<Result> DeactivateUserAsync(string username);

        Task<Result> AddRolesToUserAsync(RolesRequestDTO request);

        Task<Result> RemoveRolesFromUserAsync(RolesRequestDTO request);

        Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword);

        Task<Result> ResetPasswordAsync(string email, string token, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<Result> UpdateUserDetailsAsync(UpdateUserDetailsDTO request);

        Task<Result> UpdateUserRolesAsync(RolesRequestDTO request);
    }
}