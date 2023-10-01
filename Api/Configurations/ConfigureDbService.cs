using Application.Interfaces;
using Infrastructure.Repositories;

namespace Api.Configurations
{
    public static class ConfigureDbService
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
