using System.ComponentModel.DataAnnotations;

namespace LE.Common.Api.Paginators.Base
{
    public class BaseFilterRequest<TFilterEnum> where TFilterEnum : Enum
    {
        public TFilterEnum Filter { get; set; } = default!;

        [MaxLength(100)]
        public string? FilterQuery { get; set; } = default!;
    }
}
