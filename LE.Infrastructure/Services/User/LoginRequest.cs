using System.ComponentModel.DataAnnotations;

namespace LE.Infrastructure.Services.User
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
