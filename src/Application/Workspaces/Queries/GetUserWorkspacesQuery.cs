using AutoMapper;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Workspaces.Models;

namespace SensorFlow.Application.Workspaces.Queries
{
    // Query
    public record GetUserWorkspacesQuery(string username) : IRequest<(Result result, List<WorkspaceDTO> workspaces)>;

    // Query Handler
    public class GetUserWorkspacesQueryHandler : IRequestHandler<GetUserWorkspacesQuery, (Result result, List<WorkspaceDTO> workspaces)>
    {

        private readonly IWorkspaceRepository _workspaceRepository;
        protected readonly IMapper _mapper;

        public GetUserWorkspacesQueryHandler(IMapper mapper, IWorkspaceRepository repository)
        {
            _workspaceRepository = repository;
            _mapper = mapper;
        }

        public async Task<(Result result, List<WorkspaceDTO> workspaces)> Handle(GetUserWorkspacesQuery request, CancellationToken cancellationToken)
        {
            var result = await _workspaceRepository.GetWorkspacesByUsernameAsync(cancellationToken, request.username);

            return (result.result, _mapper.Map<List<WorkspaceDTO>>(result.workspaces));
        }
    }
}