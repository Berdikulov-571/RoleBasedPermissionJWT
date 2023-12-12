namespace RoleBasedPermissionJWT.Domain.Dtos
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public ICollection<int> Roles { get; set; }
    }
}