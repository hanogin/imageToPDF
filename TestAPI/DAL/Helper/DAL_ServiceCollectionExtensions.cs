using DAL.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Helper
{
    public static class DAL_ServiceCollectionExtensions
    {
        public static void AddDal(this IServiceCollection services)
        {
            services.AddScoped<IDapperBaseRepository, DapperBaseRepository>();

            // Repository
            //services.AddScoped<IGlobalRepository, GlobalRepository>();
            //services.AddScoped<IGroupRepository, GroupRepository>();
            //services.AddScoped<IUserMangRepository, UserManagerRepository>();
            //services.AddScoped<ISystemRepository, SystemRepository>();
        }
    }
}
