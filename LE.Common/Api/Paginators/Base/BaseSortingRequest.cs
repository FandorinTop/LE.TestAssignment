namespace LE.Common.Api.Paginators.Base
{
    public class BaseSortingRequest<TSortingEnum> where TSortingEnum : Enum
    {
        public TSortingEnum SortingEnum { get; set; } = default!;

        public bool ASC { get; set; } = true;
    }
}
