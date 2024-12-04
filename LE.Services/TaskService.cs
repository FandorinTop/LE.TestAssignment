using AutoMapper;
using LE.Common.Api.Paginators.Tasks;
using LE.Common.Api.Tasks;
using LE.Infrastructure.Repositories;
using LE.Infrastructure.Services;
using DomainTask = LE.DomainEntities.Task;
using Task = System.Threading.Tasks.Task;
using LE.DomainEntities.Base;
using LE.Common.Exceptions;
using System.Collections.Generic;

namespace LE.Services
{
    //TODO add uniq email/username validation
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _repository = taskRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateAsync(Guid userId, TaskDto dto)
        {
            var task = _mapper.Map<DomainTask>(dto, (options) => AddUserId(options, userId));
            var id = await _repository.CreateAsync(task);

            return id.ToString();
        }

        public async Task<TaskDto> GetAsync(Guid userId, Guid id)
        {
            var task = await _repository.GetAsync(id) ?? throw new RecordNotFoundException($"No task with id: '{id}'");

            if (task.UserId != userId)
            {
                throw new UnauthorizedActionException();
            }

            var dto = _mapper.Map<TaskDto>(task);

            return dto;
        }

        public async Task<TaskPaginatorResponse> GetAsync(Guid userId, TaskPaginatorRequest dto)
        {
            var response = await _repository.GetAllAsync(
                userId,
                dto.PageIndex,
                dto.PageSize,
                dto.SortingRequests,
                dto.FilterRequests
                );

            var count = await _repository.CountAsync(userId, dto.FilterRequests);
            var res = _mapper.Map<List<DomainTask>, List<TaskDto>> (new List<DomainTask>(response));


            return new TaskPaginatorResponse()
            {
                Data = res,
                PageIndex = dto.PageIndex,
                PageSize = dto.PageSize,
                TotalCount = count,
                TotalPages = count / dto.PageSize
            };
        }

        public async Task UpdateAsync(Guid userId, Guid id,  TaskDto dto)
        {
            var task = _mapper.Map<TaskDto, DomainTask>(dto, (options) => AddId(options, userId, id));

            await _repository.UpdateAsync(task);
        } 

        public async Task DeleteAsync(Guid userId, Guid id)
        {
            var task = await _repository.GetAsync(id) ?? throw new RecordNotFoundException($"No task with id: '{id}'");

            if (task.UserId != userId)
            {
                throw new UnauthorizedActionException();
            }

            await _repository.DeleteAsync(id);
        }

        private static void AddId<TSource, TDestination>(IMappingOperationOptions<TSource, TDestination> options, Guid id) where TDestination : BaseEntity
        {
            options.AfterMap((_, destination) =>
            {
                destination.Id = id;
            });
        }

        private static void AddUserId<TSource, TDestination>(IMappingOperationOptions<TSource, TDestination> options, Guid userId) where TDestination : DomainTask
        {
            options.AfterMap((_, destination) =>
            {
                destination.UserId = userId;
            });
        }

        private static void AddId<TSource, TDestination>(IMappingOperationOptions<TSource, TDestination> options, Guid userId, Guid id) where TDestination : DomainTask
        {
            options.AfterMap((_, destination) =>
            {
                destination.Id = id;
                destination.UserId = userId;
            });
        }
    }
}
