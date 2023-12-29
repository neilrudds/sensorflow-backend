using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Infrastructure.Extensions;
using SensorFlow.Infrastructure.Models.Identity;
using System.Data;

namespace SensorFlow.Infrastructure.Services.Auth
{
    internal class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;


        public ApplicationUserService(UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService, IMapper mapper)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) { return null; }

            // Get the roles

            var result = _mapper.Map<User>(user);

            return result;
        }

        public async Task<(Result result, string? userName)> GetUserNameAsync(string userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId);

            if (applicationUser == null)
            {
                return (Result.Failure("User not found!"), string.Empty);
            }

            return (Result.Success(), applicationUser.UserName);
        }

        public async Task<(Result result, string userId)> CreateUserAsync(User user, string password, List<string> roles, bool isActive)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
            applicationUser.IsActive = isActive;

            // Create identity
            var identityResult = await _userManager.CreateAsync(applicationUser, password);

            if (!identityResult.Succeeded)
            {
                return (identityResult.ToApplicationResult(), applicationUser.Id);
            }
            else if (roles?.Any() ?? false)
            {
                var rolesResult = await _userManager.AddToRolesAsync(applicationUser, roles.Select(r => r.ToUpper()));
                if (!rolesResult.Succeeded)
                {
                    return (rolesResult.ToApplicationResult(), applicationUser.Id);
                }
            }
            return (Result.Success(), applicationUser.Id);
        }

        public async Task<Result> ActivateUserAsync(string username)
        {
            var applicationUser = await _userManager.FindByNameAsync(username);

            if (applicationUser == null)
            {
                return Result.Failure("User not found!");
            }

            if (applicationUser.IsActive)
            {
                return Result.Failure("User already active!");
            }

            applicationUser.IsActive = true;
            var result = _userManager.UpdateAsync(applicationUser);

            if (result.IsCompletedSuccessfully)
            {
                return Result.Success("Sucessfully activated user!");
            }
            else
            {
                return Result.Failure("Failed to activate user!");
            }
        }

        public async Task<Result> DeactivateUserAsync(string username)
        {
            var applicationUser = await _userManager.FindByNameAsync(username);

            if (applicationUser == null)
            {
                return Result.Failure("User not found!");
            }

            if (applicationUser.IsActive)
            {
                return Result.Failure("User not active!");
            }

            applicationUser.IsActive = false;
            var result = _userManager.UpdateAsync(applicationUser);

            if (result.IsCompletedSuccessfully)
            {
                return Result.Success("Sucessfully deactivated user!");
            }
            else
            {
                return Result.Failure("Failed to deactivate user!");
            }
        }

        public async Task<Result> AddRolesToUserAsync(RolesRequestDTO request)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.userName);

            if (applicationUser == null)
            {
                return Result.Failure("User not found!");
            }

            // Need to validate the roles here, to-do!

            var result = await _userManager.AddToRolesAsync(applicationUser, request.roles.Select(r => r.ToUpper()));

            if (result.Succeeded)
            {
                return Result.Success("Sucessfully added user to role(s)!");
            }
            else
            {
                return Result.Failure("Failed to add user to role(s)!");
            }
        }

        public async Task<Result> RemoveRolesFromUserAsync(RolesRequestDTO request)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.userName);

            if (applicationUser == null)
            {
                return Result.Failure("User not found!");
            }

            // Need to validate the roles here, to-do!

            var result = await _userManager.RemoveFromRolesAsync(applicationUser, request.roles.Select(r => r.ToUpper()));

            if (result.Succeeded)
            {
                return Result.Success("Sucessfully removed user from role(s)!");
            }
            else
            {
                return Result.Failure("Failed to remove user from role(s)!");
            }
        }

        public async Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId);

            if (applicationUser == null)
            {
                return Result.Failure("User not found!");
            }

            var result = await _userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);

            if (result.Succeeded)
            {
                return Result.Success("Password changed sucessfully!");
            }
            else
            {
                return Result.Failure("Failed to change users password!");
            }
        }

        public async Task<Result> ResetPasswordAsync(string email, string token, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);

            if (applicationUser == null)
            {
                return Result.Failure("User not found!");
            }

            var result = await _userManager.ResetPasswordAsync(applicationUser, token, password);

            if (result.Succeeded)
            {
                return Result.Success(String.Format("Password sucessfully reset for user {0} !", email));
            }
            {
                return Result.Failure("Failed to reset user password");
            }
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

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<Result> UpdateUserDetailsAsync(UpdateUserDetailsDTO request)
        {
            var applicationUser = _userManager.Users.SingleOrDefault(u => u.Id == request.Id);

            if (applicationUser == null)
            {
                return Result.Failure("User not found!");
            }

            if (!string.IsNullOrWhiteSpace(request.Email) && applicationUser.Email != request.Email)
            {
                applicationUser.Email = request.Email;
            }

            if (!string.IsNullOrWhiteSpace(request.FirstName) && applicationUser.FirstName != request.FirstName)
            {
                applicationUser.FirstName = request.FirstName;
            }


            if (!string.IsNullOrWhiteSpace(request.LastName) && applicationUser.LastName != request.LastName)
            {
                applicationUser.FirstName = request.LastName;
            }


            if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && applicationUser.PhoneNumber != request.PhoneNumber)
            {
                applicationUser.PhoneNumber = request.PhoneNumber;
            }

            var result = await _userManager.UpdateAsync(applicationUser);

            if (result.Succeeded)
            {
                return Result.Success(String.Format("Details sucessfully updated for user {0} !", applicationUser.Id));
            }
            {
                return Result.Failure("Failed to update user details");
            }
        }

        public async Task<Result> UpdateUserRolesAsync(RolesRequestDTO request)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.userName);

            if (applicationUser == null)
            {
                return Result.Failure("User not found!");
            }

            var currentRoles = await _userManager.GetRolesAsync(applicationUser);
            var identityResult = await _userManager.RemoveFromRolesAsync(applicationUser, currentRoles);

            if (!identityResult.Succeeded)
            {
                return Result.Failure("Unable to update user roles.");
            }

            identityResult = await _userManager.AddToRolesAsync(applicationUser, request.roles.Select(r => r.ToUpper()));

            if (identityResult.Succeeded)
            {
                return Result.Success("Sucessfully updated user role(s)!");

            }
            else
            {
                return Result.Failure("Unable to update user roles.");
            }
        }
    }
}