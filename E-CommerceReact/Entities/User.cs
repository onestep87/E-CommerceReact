using Microsoft.AspNetCore.Identity;

namespace E_CommerceReact.Entities
{
    public class User : IdentityUser<int>
    {
        public UserAddress Address { get; set; }
    }
}
