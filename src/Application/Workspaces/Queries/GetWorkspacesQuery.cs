using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Workspaces.Models;

namespace SensorFlow.Application.Workspaces.Queries
{
    // Query
    public record GetWorkspacesQuery() : IRequest<List<WorkspaceDTO>>;

    // Query Handler
    public class GetWorkspacesQueryHandler : IRequestHandler<GetWorkspacesQuery, List<WorkspaceDTO>>
    {

        private readonly IWorkspaceRepository _workspaceRepository;
        protected readonly IMapper _mapper;

        public GetWorkspacesQueryHandler(IMapper mapper, IWorkspaceRepository repository)
        {
            _workspaceRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<WorkspaceDTO>> Handle(GetWorkspacesQuery request, CancellationToken cancellationToken)
        {
            var workspaces = await _workspaceRepository.GetAllAsync(cancellationToken);

            // to-do Guard against not found

            return _mapper.Map<List<WorkspaceDTO>>(workspaces);
        }
    }
}