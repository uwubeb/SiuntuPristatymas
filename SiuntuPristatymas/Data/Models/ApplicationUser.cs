using Microsoft.AspNetCore.Identity;
using SiuntuPristatymas.Data.Enums;

namespace SiuntuPristatymas.Data.Models
{
    public class ApplicationUser : IdentityUser
    {

        public RolesEnum Role { get; set; }

    }
}
