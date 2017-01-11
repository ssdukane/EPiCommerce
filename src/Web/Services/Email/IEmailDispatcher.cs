using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Logging;
using EPICommerce.Web.Services.Email.Models;

namespace EPICommerce.Web.Services.Email
{
    public interface IEmailDispatcher
    {
        SendEmailResponse SendEmail(Postal.Email email);
        SendEmailResponse SendEmail(Postal.Email email, ILogger log);
    }
}
