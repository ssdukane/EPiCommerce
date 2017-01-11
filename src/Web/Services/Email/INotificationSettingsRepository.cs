using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPICommerce.Web.Models.ViewModels.Email;

namespace EPICommerce.Web.Services.Email
{
    public interface INotificationSettingsRepository
    {
        NotificationSettings GetNotificationSettings(string language = null);
    }
}
