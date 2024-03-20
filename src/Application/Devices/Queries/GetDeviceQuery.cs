using AutoMapper;
using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Devices.Models;

namespace SensorFlow.Application.Devices.Queries
{
    // Query
    public record GetDeviceQuery(string deviceId) : IRequest<ErrorOr<DeviceDTO>>;

    // Query Handler
    public class GetDeviceQueryHandler : IRequestHandler<GetDeviceQuery, ErrorOr<DeviceDTO>>
    {
        private readonly IDeviceRepository _deviceRepository;
        protected readonly IMapper _mapper;

        public GetDeviceQueryHandler(IMapper mapper, IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<DeviceDTO>> Handle(GetDeviceQuery request, CancellationToken cancellationToken)
        {
            var result = await _deviceRepository.GetDeviceByIdAsync(cancellationToken, request.deviceId);

            if (result.IsError)
                return result.Errors;

            return _mapper.Map<DeviceDTO>(result.Value);
        }
    }
}
