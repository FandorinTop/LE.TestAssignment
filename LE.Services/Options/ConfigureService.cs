using Microsoft.Extensions.DependencyInjection;

namespace LE.Services.Options
{
    public static class ConfigureService
    {
        public static void AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppMappingProfile));
        }
    }
}
