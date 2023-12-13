using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBasedPermissionJWT.Domain.Dtos;
using RoleBasedPermissionJWT.Domain.Entities;
using RoleBasedPermissionJWT.Service.Abstractions.DataContexts;
using RoleBasedPermissionJWT.Service.Abstractions.Interfaces;
using RoleBasedPermissionJWT.Service.Security.Password;
using System.Security.Claims;

namespace RoleBasedPermissionJWT.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly ITokenService _jwtTokenService;

        public AccountController(IApplicationDbContext context, ITokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null)
                return NotFound("Chiqmadi");

            if (!(Hash512.ComputeSHA512HashFromString(loginDto.Password) == user.PasswordHash))
                throw new Exception("Username or password is not valid");

            return Ok(new TokenDto
            {
                AccessToken = _jwtTokenService.GenerateJWT(user),
                RefreshToken = user.RefreshToken,
                ExpireDate = user.RefreshTokenExpireDate ?? DateTime.Now.AddMinutes(2)
            });
        }

        [HttpPost]
        public async ValueTask<IActionResult> Register(RegisterDto registerDto)
        {
            RoleBasedPermissionJWT.Domain.Entities.User user = new User()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PasswordHash = Hash512.ComputeSHA512HashFromString(registerDto.Password),
                RefreshTokenExpireDate = DateTime.Now.AddMinutes(2),
                RefreshToken = Guid.NewGuid().ToString(),
                Salt = "sanjarbek"
            };

            List<Role> roles = new List<Role>();

            foreach (var role in registerDto.Roles)
            {
                var listItem = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == role);
                if (listItem == null) { /**/ }
                else
                {
                    roles.Add(listItem);
                }
            }

            user.Roles = roles;

            var entityEntry = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new TokenDto()
            {
                AccessToken = _jwtTokenService.GenerateJWT(entityEntry.Entity),
                RefreshToken = user.RefreshToken,
                ExpireDate = user.RefreshTokenExpireDate ?? DateTime.Now.AddMinutes(2),
            });
        }

        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDto refreshTokenDTO)
        {
            var token = await _jwtTokenService.RefreshToken(refreshTokenDTO);

            return Ok(token);
        }


        [Authorize(Roles = "CreateUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new
            {
                CurrentUserRole = role,
                Users = await _context.Users.ToListAsync()
            });
        }
    }
}