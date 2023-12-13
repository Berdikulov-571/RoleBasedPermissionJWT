using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RoleBasedPermissionJWT.Service.Abstractions.Interfaces;
using RoleBasedPermissionJWT.Service.Services;
using System.Reflection;

namespace RoleBasedPermissionJWT.Service
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}