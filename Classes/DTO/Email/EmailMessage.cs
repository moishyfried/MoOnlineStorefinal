using MimeKit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace MoOnlineStore.Core.DTO.Email
{
    public class EmailMessage:BaseEmail
    {
        public EmailMessage()
        {
        }

        public EmailMessage(IEnumerable<string> to, string subject, MimeMessage content, IFormFileCollection attachments = null)
        {
           
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
        [Required]
        public MimeMessage Content { get; set; }
        public IFormFileCollection Attachments { get; set; }
    }

}
