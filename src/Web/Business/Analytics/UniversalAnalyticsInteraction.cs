﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.GoogleAnalytics.Web.Tracking;


namespace EPICommerce.Web.Business.Analytics
{
    public class UniversalAnalyticsInteraction  : AnalyticsInteraction
    {
        // We're not using these, but we need the InteractionKey to be unique
        // Note! We always mark this for deletion after is has been rendered
        // to the page.
        public UniversalAnalyticsInteraction() : 
            base("overridden-interaction", true, Guid.NewGuid().ToString())
        {
            ClearWhenContextChanged = true;
        }

        public UniversalAnalyticsInteraction(string script)
            : base("overridden-interaction", true, Guid.NewGuid().ToString())
        {
            Script = script;
            ClearWhenContextChanged = true;
        }


        /// <summary>
        /// The javascript to add to the tracking script
        /// </summary>
        /// <remarks>
        /// The javascript must be complete and valid, or it might break 
        /// the whole analytics tracking feature.
        /// </remarks>
        public string Script { get; set; }

        public override string GetUAScript()
        {
            return Script;
        }
    }
}
