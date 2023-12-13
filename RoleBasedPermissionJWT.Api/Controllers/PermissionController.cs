using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBasedPermissionJWT.Domain.Dtos;
using RoleBasedPermissionJWT.Domain.Entities;
using RoleBasedPermissionJWT.Service.Abstractions.DataContexts;

namespace RoleBasedPermissionJWT.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PermissionController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        public PermissionController(IApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetPermissionsAsync()
        {
            return Ok(await _context.Permissions.AsNoTracking().ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreatePermissionDto permissionDTO)
        {
            var permission = new Permission();
            permission.PermissionName = permissionDTO.Name;

            var newPermission = await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();

            return Ok(newPermission.Entity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, UpdatePermissionDto PermissionDTO)
        {
            var permission = await _context.Permissions
                .FirstOrDefaultAsync(x => x.PermissionId == id);

            if (permission == null) { /**/ }

            permission.PermissionName = PermissionDTO.Name;

            var entityEntry = _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();

            return Ok(entityEntry.Entity);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var permission = await _context.Permissions
                .FirstOrDefaultAsync(x => x.PermissionId == id);

            if (permission == null) { /**/ }

            var entityEntry = _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();

            return Ok(entityEntry.Entity);
        }
    }
}