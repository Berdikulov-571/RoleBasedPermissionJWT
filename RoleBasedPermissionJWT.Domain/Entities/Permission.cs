using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RoleBasedPermissionJWT.Domain.Entities
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = default!;

        [JsonIgnore]
        public ICollection<Role> Roles { get; set; } 
    }
}