using BugTracker.Data;
using BugTracker.Interfaces;
using BugTracker.Repositories;
using BugTracker.Services;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Extensions
{
    public static class ApplicationServices
    {
        //contains all general app services to be addded to DI container 
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //DBContext
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            //JWT token creation
            services.AddScoped<ITokenService, TokenService>();

            //automapper - map members to dto
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
