using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.MailService
{
    public class MailLogService : IMailLogService
    {
        public async Task SendMailAsync(string to, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            message.From = new MailAddress("merveearpturk@gmail.com");

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    "merveearpturk@gmail.com",
                    "zhckncobnncjgafe"
                ),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}