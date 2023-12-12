namespace RoleBasedPermissionJWT.Domain.Dtos
{
    public class UpdateRoleDto
    {
        public string Name { get; set; }

        public ICollection<int> Permissions { get; set; }
    }
}