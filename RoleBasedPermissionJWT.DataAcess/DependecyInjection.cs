using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoleBasedPermissionJWT.DataAcess.DataContexts;
using RoleBasedPermissionJWT.Service.Abstractions.DataContexts;

namespace RoleBasedPermissionJWT.DataAcess
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<IApplicationDbContext,ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return service;
        }
    }
}