using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.Commands
{
    public record UpdateUserDetailsCommand(string Id, string email, string firstName, string lastName, string phoneNumber) : IRequest;

    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public UpdateUserDetailsCommandHandler(IApplicationUserService applicationUserService, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            await _applicationUserService.UpdateUserDetailsAsync(_mapper.Map<User>(request));
        }
    }
}