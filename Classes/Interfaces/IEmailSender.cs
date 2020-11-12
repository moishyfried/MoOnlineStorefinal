using MimeKit;
using MoOnlineStore.Core.DTO.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Core.Interfaces
{
   public interface IEmailSender
    {
        Task<bool> SendEmailAsync(MimeMessage message);
    }
}
