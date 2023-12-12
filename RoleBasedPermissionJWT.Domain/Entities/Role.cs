using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RoleBasedPermissionJWT.Domain.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}