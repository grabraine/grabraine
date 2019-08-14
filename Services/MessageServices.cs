using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;


namespace WebApi.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
                  // Plug in your email service here to send an email.
                var sendemail = new MimeMessage ();
                sendemail.From.Add (new MailboxAddress ("NO REPLY" ,"maureen@healthkinect.co"));
                sendemail.To.Add (new MailboxAddress ("healthkinect", email));
                sendemail.Subject = subject;

                sendemail.Body = new TextPart ("plain") {
                    Text = message
                };
    using (var client = new SmtpClient (new ProtocolLogger (Console.OpenStandardOutput ()))) {
                    // Connect to the server
                
        client.Connect("smtpout.secureserver.net", 80, false);
                    
                    // Authenticate ourselves towards the server
                    client.Authenticate("maureen@healthkinect.co", "Sheba003"); // should fail here
                    client.Send(sendemail);
                       return Task.FromResult(0); 
    
            }
    
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
