using LE.Common.Api.Paginators.Tasks;
using DomainTask = LE.DomainEntities.Task;

namespace LE.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        public Task<string> CreateAsync(DomainTask dto);

        public Task UpdateAsync(string id, DomainTask dto);

        public Task<DomainTask> GetAsync(string id);

        public Task<IEnumerable<DomainTask>> GetAllAsync(int index = 0, int size = 10, IEnumerable<TaskSortingRequest> sortingRequests = default!, IEnumerable<TaskFilterRequest> filterRequests = default!);
    }
}
