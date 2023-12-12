using Microsoft.EntityFrameworkCore;
using RoleBasedPermissionJWT.Domain.Entities;

namespace RoleBasedPermissionJWT.Service.Abstractions.DataContexts
{
    public interface IApplicationDbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}