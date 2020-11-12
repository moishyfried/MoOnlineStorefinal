using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MoOnlineStore.Api.Pages;
using MoOnlineStore.Core.DTO.Email;
using MoOnlineStore.Core.Interfaces;

namespace MoOnlineStore.Api.Controllers
{
    
    public class EmailController : BaseApiController
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;
        public EmailConfiguration _emailConfig { get; set; }
        public EmailController(IEmailSender emailSender, IConfiguration config)
        { 
                _config = config;
               _emailConfig = _config
              .GetSection("EmailConfiguration")
              .Get<EmailConfiguration>();
            _emailSender = emailSender;
        }
        [HttpPost("sendemail")]
        public async Task<ActionResult<bool>> SendEmail(ContactEmailDto emailDto)
        {
            var emailMessage = CreateContactEmailMessage(emailDto);
                  return  await _emailSender.SendEmailAsync(emailMessage);
        }
        private MimeMessage CreateContactEmailMessage(ContactEmailDto message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.Add(new MailboxAddress("kiryasjoelonlinestore@gmail.com"));
            emailMessage.Subject = "somone contact you";
            emailMessage.Importance = MessageImportance.High;
            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format(
           "<h2 style='color:blue;' >somone contact you!!!<br>name: {0}<br>email: {1}<br>phonenumber:{2} <br> message: {3}</h2>",message.Name,message.EmailAddress,message.PhoneNumber,message.Message) };
            emailMessage.Body = bodyBuilder.ToMessageBody();   
            return emailMessage;

        }
    }
}
    