using Microsoft.AspNetCore.Http;
using System.Numerics;
using System.Security.Claims;

namespace SensorFlow.Infrastructure.Services.Auth
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string? GetNameIdentifier()
        {
            if (_context.HttpContext.User.Identity.IsAuthenticated)
                return _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return null;
        }
    }
}
