using System.ComponentModel.DataAnnotations;

namespace LE.DomainEntities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //TODO make change at update
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
