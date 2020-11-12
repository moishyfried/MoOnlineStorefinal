using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace MoOnlineStore.Core.DTO.Email
{
    public class ContactEmailDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string  Message { get; set; }
    }
}
