using System.Text.Json.Serialization;

namespace RoleBasedPermissionJWT.Domain.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = default!;

        [JsonIgnore]
        public List<Role> Roles { get; set; } 
    }
}