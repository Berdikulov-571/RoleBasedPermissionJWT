using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBasedPermissionJWT.Domain.Dtos;
using RoleBasedPermissionJWT.Domain.Entities;
using RoleBasedPermissionJWT.Service.Abstractions.DataContexts;
using RoleBasedPermissionJWT.Service.Security.Password;

namespace RoleBasedPermissionJWT.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        public UserController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _context.Users.AsNoTracking().Include(x => x.Roles).ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, UpdateUserDto userDTO)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null) { /**/ }

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.PasswordHash = Hash512.ComputeSHA512HashFromString(userDTO.Password);

            List<Role> roles = new();

            foreach (var role in userDTO.Roles)
            {
                var listItem = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == role);
                if (listItem == null) { /**/ }
                else
                {
                    roles.Add(listItem);
                }
            }

            user.Roles = roles;

            var entityEntry = _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(entityEntry.Entity);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null) { /**/ }

            var entityEntry = _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(entityEntry.Entity);
        }
    }
}