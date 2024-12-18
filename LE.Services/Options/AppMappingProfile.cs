﻿using AutoMapper;
using LE.Common.Api.Tasks;
using DomainTask = LE.DomainEntities.Task;

namespace LE.Services.Options
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<DomainTask, TaskDto>();
            CreateMap<TaskDto, DomainTask>();
        }
    }
}
