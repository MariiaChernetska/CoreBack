using Microsoft.AspNetCore.Identity;

namespace PillarInterview.Data.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}