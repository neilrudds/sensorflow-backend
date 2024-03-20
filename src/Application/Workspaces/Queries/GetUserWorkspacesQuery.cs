using AutoMapper;
using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Workspaces.Models;
using SensorFlow.Domain.Entities.Workspaces;

namespace SensorFlow.Application.Workspaces.Queries
{
    // Query
    public record GetUserWorkspacesQuery(string username) : IRequest<ErrorOr<List<WorkspaceDTO>>>;

    // Query Handler
    public class GetUserWorkspacesQueryHandler : IRequestHandler<GetUserWorkspacesQuery, ErrorOr<List<WorkspaceDTO>>>
    {

        private readonly IWorkspaceRepository _workspaceRepository;
        protected readonly IMapper _mapper;

        public GetUserWorkspacesQueryHandler(IMapper mapper, IWorkspaceRepository repository)
        {
            _workspaceRepository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<WorkspaceDTO>>> Handle(GetUserWorkspacesQuery request, CancellationToken cancellationToken)
        {
            var getWorkspaces = await _workspaceRepository.GetWorkspacesByUsernameAsync(cancellationToken, request.username);

            if (getWorkspaces.IsError)
                return getWorkspaces.Errors;

            return _mapper.Map<List<WorkspaceDTO>>(getWorkspaces.Value);
        }
    }
}