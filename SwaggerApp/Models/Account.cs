using Microsoft.AspNetCore.Identity;

namespace SwaggerApp.Models
{
    public class Account : IdentityUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
