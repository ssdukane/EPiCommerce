﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPICommerce.Web
{
    public class HttpContextIdentityProvider : IIdentityProvider
    {
        public System.Security.Principal.IIdentity GetIdentity()
        {
            return HttpContext.Current.User.Identity;
        }
    }
}