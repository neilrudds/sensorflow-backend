using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;

namespace SensorFlow.Application.Identity.Queries
{
    // Query
    public record IsUserInRoleQuery(string userId, string role) : IRequest<bool>;

    // Query Handler
    public class IsUserInRoleQueryHandler : IRequestHandler<IsUserInRoleQuery, bool>
    {
        private readonly IApplicationUserService _applicationUserService;
        protected readonly IMapper _mapper;

        public IsUserInRoleQueryHandler(IMapper mapper, IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(IsUserInRoleQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUserService.IsInRoleAsync(request.userId, request.role);
        }
    }
}