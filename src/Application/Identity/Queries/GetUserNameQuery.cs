using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;

namespace SensorFlow.Application.Identity.Queries
{
    // Query
    public record GetUserNameQuery(string userId) : IRequest<string?>;

    // Query Handler
    public class GetUserNameQueryHandler : IRequestHandler<GetUserNameQuery, string?>
    {
        private readonly IApplicationUserService _applicationUserService;
        protected readonly IMapper _mapper;

        public GetUserNameQueryHandler(IMapper mapper, IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public async Task<string?> Handle(GetUserNameQuery request, CancellationToken cancellationToken)
        {
            var userName = await _applicationUserService.GetUserNameAsync(request.userId);

            // to-do Guard against not found

            return userName;
        }
    }
}