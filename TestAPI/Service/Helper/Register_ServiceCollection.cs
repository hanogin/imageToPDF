using Microsoft.Extensions.DependencyInjection;

namespace Service.Helper
{
    public static class Register_ServiceCollection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<AccountService>();
        }
    }
}
