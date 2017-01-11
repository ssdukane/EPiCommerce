﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System.Web;
using EPiServer.Web;

namespace EPICommerce.Web.Business.Channels
{
    /// <summary>
    /// Defines the 'Web' content channel
    /// </summary>
    public class WebChannel : DisplayChannel
    {
        public override string ChannelName
        {
            get
            {
                return "web";
            }
        }

        public override bool IsActive(HttpContextBase context)
        {
            return !context.Request.Browser.IsMobileDevice;
        }
    }
}
