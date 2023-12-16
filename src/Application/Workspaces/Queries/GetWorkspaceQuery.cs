using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Workspaces.Models;

namespace SensorFlow.Application.Workspaces.Queries
{
    // Query
    public record GetWorkspaceQuery(Guid workspaceId) : IRequest<WorkspaceDTO>;

    // Query Handler
    public class GetWorkspaceQueryHandler : IRequestHandler<GetWorkspaceQuery, WorkspaceDTO>
    {

        private readonly IWorkspaceRepository _workspaceRepository;
        protected readonly IMapper _mapper;

        public GetWorkspaceQueryHandler(IMapper mapper, IWorkspaceRepository repository)
        {
            _workspaceRepository = repository;
            _mapper = mapper;
        }

        public async Task<WorkspaceDTO> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
        {
            var workspace = await _workspaceRepository.GetWorkspaceByIdAsync(cancellationToken, request.workspaceId);

            // to-do Guard against not found

            return _mapper.Map<WorkspaceDTO>(workspace);
        }
    }
}