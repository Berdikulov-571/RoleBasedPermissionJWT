using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBasedPermissionJWT.Domain.Dtos;
using RoleBasedPermissionJWT.Domain.Entities;
using RoleBasedPermissionJWT.Service.Abstractions.DataContexts;

namespace RoleBasedPermissionJWT.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        public RoleController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesAsync()
        {
            return Ok(await _context.Roles.AsNoTracking().Include(x => x.Permissions).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateRoleDto roleDTO)
        {
            var role = new Role();
            role.RoleName = roleDTO.Name;

            List<Permission> permissions = new();

            foreach (var permission in roleDTO.Permissions)
            {
                var storagePermission = await _context.Permissions
                    .FirstOrDefaultAsync(x => x.PermissionId == permission);

                if (storagePermission == null)
                { /**/ }
                else
                    permissions.Add(storagePermission);
            }

            role.Permissions = permissions;

            var newRole = await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return Ok(newRole.Entity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, UpdateRoleDto roleDTO)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(x => x.RoleId == id);

            if (role == null) { /**/ }

            role.RoleName = roleDTO.Name;

            List<Permission> permissions = new();

            foreach (var permission in roleDTO.Permissions)
            {
                var storagePermission = await _context.Permissions
                    .FirstOrDefaultAsync(x => x.PermissionId == permission);

                if (storagePermission == null)
                { /**/ }
                else
                    permissions.Add(storagePermission);
            }

            role.Permissions = permissions;

            var entityEntry = _context.Roles.Update(role);
            await _context.SaveChangesAsync();

            return Ok(entityEntry.Entity);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(x => x.RoleId == id);

            if (role == null) { /**/ }

            var entityEntry = _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(entityEntry.Entity);
        }
    }
}