using LE.Infrastructure.Services.Task;

namespace LE.Infrastructure.Services
{
    public interface ITaskService
    {
        public Task<string> CreateAsync(TaskDto dto);

        public Task UpdateAsync(Guid id, TaskDto dto);

        public Task Get(Guid id);

        public Task<GetPaginatorResponse<TaskDto>> Get(GetPaginatorRequest dto);
    }
}
