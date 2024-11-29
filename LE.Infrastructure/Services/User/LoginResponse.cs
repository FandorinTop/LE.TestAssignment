namespace LE.Infrastructure.Services.User
{
    public class LoginResponse
    {
        public string AuthToken { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
    }
}
