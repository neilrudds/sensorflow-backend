using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Identity.Models;

namespace SensorFlow.Application.Identity.Queries
{
    // Query
    public record GetUserQuery(string userId) : IRequest<UserDTO>;

    // Query Handler
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {
        private readonly IApplicationUserService _applicationUserService;
        protected readonly IMapper _mapper;

        public GetUserQueryHandler(IMapper mapper, IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetUserByIdAsync(request.userId);
            return _mapper.Map<UserDTO>(user);
        }
    }
}