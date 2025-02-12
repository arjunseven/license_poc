using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Ai_Inside.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<License> Licenses { get; set; } = new List<License>(); // Link user to licenses
    }
}