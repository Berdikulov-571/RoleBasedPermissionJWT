using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleBasedPermissionJWT.Domain.Entities
{
    public class UserRoles
    {
        [Key]
        public int UserRoleId { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}