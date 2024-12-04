
namespace LE.Services.Options
{
    public class JwtOption
    {
        public string ValidAudience { get; set; } = default!;
        public string ValidIssuer { get; set; } = default!;
        public string Secret { get; set; } = default!;
        public int TokenValidityInHours { get; set; } = default!;
    }
}
