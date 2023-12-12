using Microsoft.EntityFrameworkCore;
using RoleBasedPermissionJWT.Domain.Entities;
using RoleBasedPermissionJWT.Service.Abstractions.DataContexts;

namespace RoleBasedPermissionJWT.DataAcess.DataContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
    }
}