using System.ComponentModel.DataAnnotations;

namespace LE.DomainEntities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
