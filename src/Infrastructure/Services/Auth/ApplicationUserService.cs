using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Domain.Entities.Users;
using System.Data;

namespace SensorFlow.Infrastructure.Services.Auth
{
    internal class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public ApplicationUserService(UserManager<User> userManager,
        IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService, IMapper mapper)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<User?> GetUserNameByIdAsync(string userId)
        {
           return await _userManager.FindByIdAsync(userId);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ErrorOr<User>> CreateUserAsync(User user, string password, List<string> roles, bool isActive)
        {
            user.IsActive = isActive;

            // Create identity
            var identityResult = await _userManager.CreateAsync(user, password);

            if (!identityResult.Succeeded)
            {
                return Error.Failure(description: "Unable to create user");
            }
            else if (roles?.Any() ?? false)
            {
                var rolesResult = await _userManager.AddToRolesAsync(user, roles.Select(r => r.ToUpper()));
                if (!rolesResult.Succeeded)
                {
                    return Error.Failure(description: "Unable to add user roles");
                }
            }
            return user;
        }

        public async Task<ErrorOr<User?>> ActivateUserAsync(string username)
        {
            var applicationUser = await _userManager.FindByNameAsync(username);

            if (applicationUser == null)
            {
                return Error.NotFound(description: "User not found");
            }
            applicationUser.IsActive = true;
            await _userManager.UpdateAsync(applicationUser);

            return applicationUser;
        }

        public async Task<ErrorOr<User?>> DeactivateUserAsync(string username)
        {
            var applicationUser = await _userManager.FindByNameAsync(username);

            if (applicationUser == null)
            {
                return Error.NotFound(description: "User not found");
            }
            applicationUser.IsActive = false;
            await _userManager.UpdateAsync(applicationUser);

            return applicationUser;
        }

        public async Task<ErrorOr<User?>> AddRolesToUserAsync(RolesRequestDTO request)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.userName);

            if (applicationUser == null)
            {
                return Error.NotFound(description: "User not found");
            }
            await _userManager.AddToRolesAsync(applicationUser, request.roles.Select(r => r.ToUpper()));

            return applicationUser;
        }

        public async Task<ErrorOr<User?>> RemoveRolesFromUserAsync(RolesRequestDTO request)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.userName);

            if (applicationUser == null)
            {
                return Error.NotFound(description: "User not found");
            }
            await _userManager.RemoveFromRolesAsync(applicationUser, request.roles.Select(r => r.ToUpper()));

            return applicationUser;
        }

        public async Task<ErrorOr<User?>> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId);

            if (applicationUser == null)
            {
                return Error.NotFound(description: "User not found");
            }
            await _userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);

            return applicationUser;
        }

        public async Task<ErrorOr<User?>> ResetPasswordAsync(string email, string token, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);

            if (applicationUser == null)
            {
                return Error.NotFound(description: "User not found");
            }
            var result = await _userManager.ResetPasswordAsync(applicationUser, token, password);

            return applicationUser;
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null) { return false; }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            await DeleteUserAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task UpdateUserDetailsAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<ErrorOr<User?>> UpdateUserRolesAsync(RolesRequestDTO request)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.userName);

            if (applicationUser == null)
            {
                return Error.NotFound(description: "User not found");
            }

            var currentRoles = await _userManager.GetRolesAsync(applicationUser);
            var identityResult = await _userManager.RemoveFromRolesAsync(applicationUser, currentRoles);

            if (!identityResult.Succeeded)
            {
                return Error.Failure(description: "Unable to update user roles.");
            }

            await _userManager.AddToRolesAsync(applicationUser, request.roles.Select(r => r.ToUpper()));
            return applicationUser;

        }
    }
}