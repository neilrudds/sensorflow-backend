using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorFlow.Application.Identity.Commands
{
    public record UpdateUserDetailsCommand(string Id, string email, string firstName, string lastName, string phoneNumber) : IRequest<Result>;

    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public UpdateUserDetailsCommandHandler(IApplicationUserService applicationUserService, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            return await _applicationUserService.UpdateUserDetailsAsync(_mapper.Map<UpdateUserDetailsDTO>(request));
        }
    }
}
