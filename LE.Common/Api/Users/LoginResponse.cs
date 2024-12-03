namespace LE.Common.Api.Users
{
    public class LoginResponse
    {
        public string AuthToken { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
    }
}
