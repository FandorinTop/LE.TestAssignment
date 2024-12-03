using AutoMapper;
using LE.Common.Api.Tasks;
using DomainTask = LE.DomainEntities.Task;

namespace LE.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<DomainTask, TaskDto>();
        }
    }
}
