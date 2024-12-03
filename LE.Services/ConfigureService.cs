using Microsoft.Extensions.DependencyInjection;

namespace LE.Services
{
    public static class ConfigureService
    {
        public static void AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppMappingProfile));
        }
    }
}
