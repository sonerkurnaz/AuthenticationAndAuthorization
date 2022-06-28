using AuthenticationAndAuthorization.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.Models.DTOs
{
    public class AssignedRoleDTO
    {
        public IdentityRole Role { get; set; }
        public IList<AppUser> HasRole { get; set; }
        public IList<AppUser> HasNotRole { get; set; }

        public string RoleName { get; set; }
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }


    }
}
