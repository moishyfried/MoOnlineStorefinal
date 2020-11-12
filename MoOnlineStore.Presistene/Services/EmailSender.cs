using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MoOnlineStore.Core.DTO.Email;
using MoOnlineStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public EmailConfiguration _emailConfig { get; set; }
        public EmailSender( IConfiguration config)
        {
            _config = config;
            _emailConfig = _config
           .GetSection("EmailConfiguration")
           .Get<EmailConfiguration>();
        }

        public async Task<bool> SendEmailAsync(MimeMessage message)
        {
            return  await SendAsync(message);
        }
      
        private async Task<bool> SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                     await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                
                    await client.DisconnectAsync(true);
                    client.Dispose();
                      return true;
               }
        }
}
}

















  //private void SendConfimationEmail(SignUp user)
    //{
    //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
    //    string companyName = "PersonallFinnace";
    //    string sender = "mofried7@gmail.com";
    //    string password = "fraidyfried";
    //    MailAddress senderEmail = new MailAddress("mofried7@gmail.com", companyName.Trim());
    //    MailAddress recieveremail = new MailAddress(user.Email.Trim(), user.Email.Trim());
    //    var subject = "THANK YOU FOR CREATING AN ACCOUNT BY PERSONALLFINNACE";
    //    var view = string.Format("<h1>Dear {0}</h1>,<br /><h3>Thank you for signing up. We Hope For A New Good Time For You</h3> <br /> Thank You From the team at {1}.",
    //         user.Email.Trim(),
    //       companyName.Trim());
    //    SmtpClient smtp = new SmtpClient
    //    {
    //        Host = "smtp.gmail.com",
    //        Port = 587,
    //        EnableSsl = true,
    //        DeliveryMethod = SmtpDeliveryMethod.Network,
    //        UseDefaultCredentials = false,
    //        Credentials = new NetworkCredential(sender.Trim(), password.Trim())
    //    };
    //    MailMessage SendMessage = new MailMessage(senderEmail, recieveremail)
    //    {
    //        IsBodyHtml = true,
    //        Subject = subject,
    //        Body = view
    //    };
    //    smtp.Send(SendMessage);
    //}