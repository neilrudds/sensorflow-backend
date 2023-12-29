using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SensorFlow.Infrastructure.Services.Auth
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;
        //private readonly UserManager<ApplicationUser> _userManager;
        public UserResolverService(IHttpContextAccessor context/*, UserManager<ApplicationUser> userManager*/)
        {
            _context = context;
            //_userManager = userManager;
        }
        //public async Task<ApplicationUser> GetUser()
        //{
        //    return await _userManager.FindByEmailAsync(_context.HttpContext.User?.Identity?.Name);
        //}

        public string GetNameIdentifier()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
