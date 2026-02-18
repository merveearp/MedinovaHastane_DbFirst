using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.MailService
{
    public interface IMailLogService
    {
        Task SendMailAsync(string to, string subject, string body);
    }
}
