using LE.Common.Api.Paginators.Tasks;
using DomainTask = LE.DomainEntities.Task;

namespace LE.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        public Task<Guid> CreateAsync(DomainTask dto);

        public Task UpdateAsync(DomainTask dto);

        public Task<DomainTask?> GetAsync(Guid id);

        public Task DeleteAsync(Guid id);

        public Task<IEnumerable<DomainTask>> GetAllAsync(Guid userId, int index = 0, int size = 10, IEnumerable<TaskSortingRequest> sortingRequests = default!, IEnumerable<TaskFilterRequest> filterRequests = default!);

        public Task<int> CountAsync(Guid userId, IEnumerable<TaskFilterRequest> filterRequests = default!);
    }
}
