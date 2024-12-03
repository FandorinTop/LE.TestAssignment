using System.ComponentModel.DataAnnotations;

namespace LE.Common.Api.Users
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = default!;
    }
}
