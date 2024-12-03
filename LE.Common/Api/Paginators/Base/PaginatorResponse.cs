namespace LE.Common.Api.Paginators.Base
{
    public class BasePaginatorResponse<TData>
    {
        public IEnumerable<TData> Data { get; set; } = new List<TData>();

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 0;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return PageIndex + 1 < TotalPages;
            }
        }
    }
}
