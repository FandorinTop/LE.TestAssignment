using LE.Common.Api.Paginators.Tasks;
using LE.Infrastructure.Repositories;
using DomainTask = LE.DomainEntities.Task;
using LE.DataAccess;
using LE.Common.Api.Paginators.Tasks.Enums;
using LE.Common.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace LE.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(Guid userId, IEnumerable<TaskFilterRequest> filterRequests = null!)
        {
            var dataQuery = AddFilters(_context.Tasks, filterRequests);

            return await dataQuery
                .Where(item => item.UserId == userId)
                .AsNoTracking()
                .CountAsync();
        }

        public async Task<Guid> CreateAsync(DomainTask entity)
        {
            await _context.Tasks.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<DomainTask?> GetAsync(Guid id)
        {
            var entity = await _context.Tasks.FindAsync(id);

            return entity;
        }

        public async Task UpdateAsync(DomainTask task)
        {
            _context.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DomainTask>> GetAllAsync(Guid userId, int index = 0, int size = 10, IEnumerable<TaskSortingRequest> sortingRequests = null!, IEnumerable<TaskFilterRequest> filterRequests = null!)
        {
            var dataQuery = _context.Tasks
                .Where(item => item.UserId == userId)
                .Skip(size * index)
                .Take(size);

            dataQuery = AddFilters(dataQuery, filterRequests);
            dataQuery = AddSorting(dataQuery, sortingRequests);

            return await dataQuery.ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task is null)
                return;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        #region Sorting
        private IQueryable<DomainTask> AddSorting(IQueryable<DomainTask> dataQuery, IEnumerable<TaskSortingRequest> sortingRequests)
        {
            IOrderedQueryable<DomainTask> orderedQuery = default!;

            foreach (var sortingRequest in sortingRequests)
            {
                if (orderedQuery is null)
                    orderedQuery = AddSorting(dataQuery, sortingRequest);
                else
                    orderedQuery = AddSorting(orderedQuery, sortingRequest);
            }

            return orderedQuery ?? dataQuery;
        }

        private IOrderedQueryable<DomainTask> AddSorting(IQueryable<DomainTask> dataQuery, TaskSortingRequest sortingRequest)
        {
            if (sortingRequest.ASC)
                return AddASCSorting(dataQuery, sortingRequest.SortingEnum);

            return AddDESCSorting(dataQuery, sortingRequest.SortingEnum);
        }

        private IOrderedQueryable<DomainTask> AddSorting(IOrderedQueryable<DomainTask> dataQuery, TaskSortingRequest sortingRequest)
        {
            if (sortingRequest.ASC)
                return AddASCSorting(dataQuery, sortingRequest.SortingEnum);

            return AddDESCSorting(dataQuery, sortingRequest.SortingEnum);
        }

        private IOrderedQueryable<DomainTask> AddDESCSorting(IQueryable<DomainTask> dataQuery, TaskSorting sortingEnum)
        {
            switch (sortingEnum)
            {
                case TaskSorting.Status:
                    return dataQuery.OrderByDescending(item => item.Status);
                case TaskSorting.DueDate:
                    return dataQuery.OrderByDescending(item => item.DueDate);
                case TaskSorting.Priority:
                    return dataQuery.OrderByDescending(item => item.Priority);
            }

            throw new NotImplementedException();
        }

        private IOrderedQueryable<DomainTask> AddDESCSorting(IOrderedQueryable<DomainTask> dataQuery, TaskSorting sortingEnum)
        {
            switch (sortingEnum)
            {
                case TaskSorting.Status:
                    return dataQuery.ThenByDescending(item => item.Status);
                case TaskSorting.DueDate:
                    return dataQuery.ThenByDescending(item => item.DueDate);
                case TaskSorting.Priority:
                    return dataQuery.ThenByDescending(item => item.Priority);
            }

            throw new NotImplementedException();
        }

        private IOrderedQueryable<DomainTask> AddASCSorting(IQueryable<DomainTask> dataQuery, TaskSorting sortingEnum)
        {
            switch (sortingEnum)
            {
                case TaskSorting.Status:
                    return dataQuery.OrderBy(item => item.Status);
                case TaskSorting.DueDate:
                    return dataQuery.OrderBy(item => item.DueDate);
                case TaskSorting.Priority:
                    return dataQuery.OrderBy(item => item.Priority);
            }

            throw new NotImplementedException();
        }

        private IOrderedQueryable<DomainTask> AddASCSorting(IOrderedQueryable<DomainTask> dataQuery, TaskSorting sortingEnum)
        {
            switch (sortingEnum)
            {
                case TaskSorting.Status:
                    return dataQuery.ThenBy(item => item.Status);
                case TaskSorting.DueDate:
                    return dataQuery.ThenBy(item => item.DueDate);
                case TaskSorting.Priority:
                    return dataQuery.ThenBy(item => item.Priority);
            }

            throw new NotImplementedException();
        }
        #endregion

        #region Filters
        private IQueryable<DomainTask> AddFilters(IQueryable<DomainTask> dataQuery, IEnumerable<TaskFilterRequest> filterRequests)
        {
            foreach (var filterRequest in filterRequests)
            {
                dataQuery = AddFilter(dataQuery, filterRequest);
            }

            return dataQuery;
        }

        private IQueryable<DomainTask> AddFilter(IQueryable<DomainTask> dataQuery, TaskFilterRequest filterRequest)
        {
            switch (filterRequest.Filter)
            {
                case TaskFilter.DueDate:
                    return dataQuery.Where(item => item.DueDate > Convert.ToDateTime(filterRequest.FilterQuery));
                case TaskFilter.Priority:
                    return dataQuery.Where(item => item.Priority == Enum.Parse<Priority>(filterRequest.FilterQuery));
            }

            throw new NotImplementedException();
        }
        #endregion
    }
}
