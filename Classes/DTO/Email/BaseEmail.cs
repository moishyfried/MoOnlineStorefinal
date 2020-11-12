using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Core.DTO.Email
{
  public class BaseEmail
    {
        public List<string> SendToEmailAddress { get; set; }
        public string Subject { get; set; }

    }
}
