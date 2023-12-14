using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SensorFlow.Infrastructure.Services.Auth
{
    internal class UserAuthenticationService : IUserAuthenticationService
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserAuthenticationService(/*SignInManager<ApplicationUser> signInManager,*/ UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            //signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Result> Login(LoginRequestDTO request)
        {
            try
            {
                var applicationUser = await _userManager.FindByNameAsync(request.userName);
                
                if (applicationUser == null)
                    return Result.Failure("User not found");

                if (!await _userManager.CheckPasswordAsync(applicationUser, request.password))
                    return Result.Failure("Invalid password");

                var userRoles = await _userManager.GetRolesAsync(applicationUser);
                var authClaims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name, applicationUser.UserName),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                string token = GenerateToken(authClaims);
                return Result.Success(token);

            }
            catch (Exception ex)
            {
                return Result.Failure("Unable to sign in user");
            }
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                //Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Task<Result> Logout()
        {
            throw new NotImplementedException();
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
