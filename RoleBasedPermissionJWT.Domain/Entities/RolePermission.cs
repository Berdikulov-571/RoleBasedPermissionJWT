using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleBasedPermissionJWT.Domain.Entities
{
    public class RolePermission
    {
        [Key]
        public int RolePermissionId { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        [ForeignKey(nameof(Permission))]
        public int PermissionId { get; set; }


        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}