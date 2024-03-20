using AutoMapper;
using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Devices.Models;

namespace SensorFlow.Application.Devices.Queries
{
    // Query
    public record GetWorkspaceDevicesQuery(string workspaceId) : IRequest<ErrorOr<List<DeviceDTO>>>;

    // Query Handler
    public class GetWorkspaceDevicesQueryHandler : IRequestHandler<GetWorkspaceDevicesQuery, ErrorOr<List<DeviceDTO>>>
    {
        private readonly IDeviceRepository _deviceRepository;
        protected readonly IMapper _mapper;

        public GetWorkspaceDevicesQueryHandler(IMapper mapper, IDeviceRepository repository)
        {
            _deviceRepository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<DeviceDTO>>> Handle(GetWorkspaceDevicesQuery request, CancellationToken cancellationToken)
        {
            var result = await _deviceRepository.GetDevicesByWorkspaceIdAsync(cancellationToken, request.workspaceId);

            if (result.IsError)
                return result.Errors;

            return _mapper.Map<List<DeviceDTO>>(result.Value);
        }
    }
}
