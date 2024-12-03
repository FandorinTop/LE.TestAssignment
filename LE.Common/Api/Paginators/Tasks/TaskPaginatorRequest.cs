using LE.Common.Api.Paginators.Base;

namespace LE.Common.Api.Paginators.Tasks
{
    public class TaskPaginatorRequest : BasePaginatorRequest
    {
        public IEnumerable<TaskSortingRequest> SortingRequests { get; set; } = new List<TaskSortingRequest>();

        public IEnumerable<TaskFilterRequest> FilterRequests { get; set; } = new List<TaskFilterRequest>();
    }
}
