/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPICommerce.Web.Business.ClientTracking;
using EPICommerce.Web.Models.Blocks;
using EPICommerce.Web.Models.PageTypes;
using EPICommerce.Web.Models.ViewModels;

namespace EPICommerce.Web.Controllers
{
    public class BannerBlockController : BlockController<BannerBlock>
    {
        private readonly IGoogleAnalyticsTracker _googleAnalyticsTracker;

        public BannerBlockController(IGoogleAnalyticsTracker googleAnalyticsTracker)
        {
            _googleAnalyticsTracker = googleAnalyticsTracker;
        }

        public override ActionResult Index(BannerBlock currentBlock)
        {
            if(string.IsNullOrEmpty(currentBlock.PromotionId) == false ||
               string.IsNullOrEmpty(currentBlock.PromotionName) == false)
            {
                _googleAnalyticsTracker.TrackPromotionImpression(currentBlock.PromotionId, currentBlock.PromotionName, currentBlock.PromotionBannerName);
            }

            return View("_bannerblock", currentBlock);
        }


    }
}
