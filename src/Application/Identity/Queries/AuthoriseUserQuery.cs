using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;

namespace SensorFlow.Application.Identity.Queries
{
    // Query
    public record AuthoriseUserQuery(string userId, string policyName) : IRequest<bool>;

    // Query Handler
    public class AuthoriseUserQueryHandler : IRequestHandler<AuthoriseUserQuery, bool>
    {
        private readonly IApplicationUserService _applicationUserService;
        protected readonly IMapper _mapper;

        public AuthoriseUserQueryHandler(IMapper mapper, IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AuthoriseUserQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUserService.AuthorizeAsync(request.userId, request.policyName);
        }
    }
}