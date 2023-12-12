namespace RoleBasedPermissionJWT.Domain.Dtos
{
    public class CreateRoleDto
    {
        public string Name { get; set; }
        public List<int> Permissions { get; set; }
    }
}