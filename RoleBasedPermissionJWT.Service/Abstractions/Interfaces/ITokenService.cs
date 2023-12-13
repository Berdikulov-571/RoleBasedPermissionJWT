using RoleBasedPermissionJWT.Domain.Dtos;
using RoleBasedPermissionJWT.Domain.Entities;

namespace RoleBasedPermissionJWT.Service.Abstractions.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJWT(User user);
        public ValueTask<TokenDto> RefreshToken(RefreshTokenDto refreshTokenDTO);
    }
}