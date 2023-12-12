using System.Text.Json.Serialization;

namespace RoleBasedPermissionJWT.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        [JsonIgnore]
        public 
    }
}