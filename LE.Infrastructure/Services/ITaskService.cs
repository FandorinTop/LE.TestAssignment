using LE.Common.Api.Paginators.Tasks;
using LE.Common.Api.Tasks;

namespace LE.Infrastructure.Services
{
    public interface ITaskService
    {
        public Task<string> CreateAsync(TaskDto dto);

        public Task UpdateAsync(Guid id, TaskDto dto);

        public Task<TaskDto> Get(Guid id);

        public Task<TaskPaginatorResponse> Get(TaskPaginatorRequest dto);
    }
}
