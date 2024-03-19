using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Devices.Models;

namespace SensorFlow.Application.Devices.Queries
{
    // Query
    public record GetWorkspaceDevicesQuery(string workspaceId) : IRequest<(Result result, List<DeviceDTO> devices)>;

    // Query Handler
    public class GetWorkspaceDevicesQueryHandler : IRequestHandler<GetWorkspaceDevicesQuery, (Result result, List<DeviceDTO> devices)>
    {
        private readonly IDeviceRepository _deviceRepository;
        protected readonly IMapper _mapper;

        public GetWorkspaceDevicesQueryHandler(IMapper mapper, IDeviceRepository repository)
        {
            _deviceRepository = repository;
            _mapper = mapper;
        }

        public async Task<(Result result, List<DeviceDTO> devices)> Handle(GetWorkspaceDevicesQuery request, CancellationToken cancellationToken)
        {
            var result = await _deviceRepository.GetDevicesByWorkspaceIdAsync(cancellationToken, request.workspaceId);

            // to-do Guard against not found

            return (result.result, _mapper.Map<List<DeviceDTO>>(result.devices));
        }
    }
}
