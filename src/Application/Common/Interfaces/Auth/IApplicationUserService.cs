using ErrorOr;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Domain.Entities.Users;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */
namespace SensorFlow.Application.Common.Interfaces
{
    public interface IApplicationUserService
    {
        Task<User?> GetUserByIdAsync(string userId);

        Task<User?> GetUserNameByIdAsync(string userId);

        Task<User?> GetUserByUserNameAsync(string userName);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<ErrorOr<User>> CreateUserAsync(User user, string password, List<string> roles, bool isActive);

        Task<ErrorOr<User?>> ActivateUserAsync(string username);

        Task<ErrorOr<User?>> DeactivateUserAsync(string username);

        Task<ErrorOr<User?>> AddRolesToUserAsync(RolesRequestDTO request);

        Task<ErrorOr<User?>> RemoveRolesFromUserAsync(RolesRequestDTO request);

        Task<ErrorOr<User?>> ChangePasswordAsync(string userId, string oldPassword, string newPassword);

        Task<ErrorOr<User?>> ResetPasswordAsync(string email, string token, string password);

        Task DeleteUserAsync(string userId);

        Task UpdateUserDetailsAsync(User user);

        Task<ErrorOr<User?>> UpdateUserRolesAsync(RolesRequestDTO request);
    }
}