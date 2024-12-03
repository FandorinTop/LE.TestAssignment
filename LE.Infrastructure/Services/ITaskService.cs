using LE.Common.Api.Paginators.Tasks;
using LE.Common.Api.Tasks;

namespace LE.Infrastructure.Services
{
    public interface ITaskService
    {
        public Task<string> CreateAsync(Guid userId, TaskDto dto);

        public Task UpdateAsync(Guid userId, Guid id, TaskDto dto);

        public Task<TaskDto> GetAsync(Guid userId, Guid id);

        public Task DeleteAsync(Guid userId, Guid id);

        public Task<TaskPaginatorResponse> GetAsync(Guid userId, TaskPaginatorRequest dto);
    }
}
