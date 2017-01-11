﻿using System.Web;
using EPiServer.Shell.Web;

namespace EPICommerce.Web
{
    public interface IHttpContextProvider
    {
        HttpContextBase GetContext();
    }

    public class HttpContextProvider : IHttpContextProvider
    {

        public HttpContextBase GetContext()
        {
            return HttpContext.Current.GetHttpContextBase();
        }
    }
}
