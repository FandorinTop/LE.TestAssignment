namespace LE.Common.Api.Paginators.Base
{
    public class BasePaginatorRequest
    {
        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10;
    }
}
