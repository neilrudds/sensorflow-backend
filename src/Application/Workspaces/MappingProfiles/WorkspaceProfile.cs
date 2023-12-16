using AutoMapper;
using SensorFlow.Application.Workspaces.Models;
using SensorFlow.Domain.Entities.Workspaces;

namespace SensorFlow.Application.Workspaces.MappingProfiles
{
    public sealed class WorkspaceProfile : Profile
    {
        public WorkspaceProfile()
        {
            CreateMap<Workspace, WorkspaceDTO>();
        }
    }
}