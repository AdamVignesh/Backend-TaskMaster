using Microsoft.AspNetCore.Identity;

namespace capstone.Models
{
    public class UserModel : IdentityUser
    {
        public String Role { get; set; }
        public String ImgUrl { get; set; }
    }
}
