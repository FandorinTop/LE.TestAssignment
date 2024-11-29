using System.ComponentModel.DataAnnotations;
using LE.DomainEntities.Base;

namespace LE.DomainEntities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = default!;

        public string PasswordHash { get; set; } = default!;

        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
