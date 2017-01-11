using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPICommerce.Web.Models.PageTypes;

namespace EPICommerce.Web.Business
{
    public interface ISiteSettingsProvider
    {
        SettingsBlock GetSettings();
        HomePage GetStartPage();
    }
}
