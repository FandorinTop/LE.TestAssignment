using System.ComponentModel.DataAnnotations;

namespace LE.Common.Api.Users
{
    public class UserDto
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = default!;
    }
}
