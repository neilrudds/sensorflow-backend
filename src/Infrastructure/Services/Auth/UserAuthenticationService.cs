using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using SensorFlow.Domain.Entities.Users;
using ErrorOr;

namespace SensorFlow.Infrastructure.Services.Auth
{
    internal class UserAuthenticationService : IUserAuthenticationService
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserAuthenticationService(/*SignInManager<ApplicationUser> signInManager,*/ UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            //signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ErrorOr<LoginResponseDTO>> Login(LoginRequestDTO request)
        {
            var result = new LoginResponseDTO();
            try
            {
                var user = await _userManager.FindByNameAsync(request.userName);

                if (user == null)
                    return Error.NotFound(description: "User not found");

                if (!await _userManager.CheckPasswordAsync(user, request.password))
                    return Error.Failure(description: "Invalid password");

                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                
                var token = GenerateToken(authClaims);
                return CreateLoginResponse(user, true, token.Item1, token.Item2);

            }
            catch (Exception ex)
            {
                return Error.Failure(description: ex.ToString());
            }
        }

        private (string, DateTime) GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var _TokenExpiryTime = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = _TokenExpiryTime,
                //Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), _TokenExpiryTime);
        }

        public Task<Error> Logout()
        {
            throw new NotImplementedException();
        }

        private LoginResponseDTO CreateLoginResponse(User user, bool success = false, string jwtToken = "", DateTime? jwtExpiry = null)
        {
            var loginResponse = new LoginResponseDTO()
            {
                userName = user.Email,
                success = success,
                requires2FA = false,
                isLockedOut = user.LockoutEnabled,
                jwtToken = jwtToken,
                jwtExpiry = jwtExpiry
            };
            return loginResponse;
        }


        //public async Task<Result> Login(LoginRequestDTO request)
        //{
        //    try
        //    {
        //        var applicationUser = await _userManager.FindByNameAsync(request.userName);

        //        var loginResult = await _signInManager.PasswordSignInAsync(applicationUser, request.password, false, true);

        //        // To-do finish this
        //        return Result.Success("");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result.Failure("Unable to sigin in user");
        //    }
        //}

        //public async Task<Result> Logout()
        //{
        //    try
        //    {
        //        await _signInManager.SignOutAsync();

        //        return Result.Success("");
        //    }
        //    catch (Exception ex) 
        //    {
        //        return Result.Failure("Failed to sign out user");

        //    }
    }
}
