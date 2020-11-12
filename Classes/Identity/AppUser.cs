using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;

namespace MoOnlineStore.Core.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address address { get; set; }
    }
}