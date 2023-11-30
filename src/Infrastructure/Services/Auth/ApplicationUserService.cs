using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Infrastructure.Extensions;
using SensorFlow.Infrastructure.Models.Identity;

namespace SensorFlow.Infrastructure.Services.Auth
{
    public class ApplicationUserService : IApplicationUserService
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

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(User user, string password, List<string> roles, bool isActive)
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

    }
}