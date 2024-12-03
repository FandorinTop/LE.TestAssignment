using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LE.Common.Entities.Enums;
using LE.DomainEntities.Base;

namespace LE.DomainEntities
{
    public class Task : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = default!;

        [MaxLength(300)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public Priority Priority { get; set; } = Priority.Low;

        public Status Status { get; set; } = Status.None;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = default!;
        
        public User User { get; set; } = default!;
    }
}
