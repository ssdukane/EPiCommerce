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
using System.Web.Mvc;
using EPiServer.Commerce.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Shell.Rest.Query;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Localization;
using EPiServer.Framework.Web.Mvc;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Inventory;
using Mediachase.Commerce.Pricing;
using EPICommerce.Core.Extensions;
using EPICommerce.Web.Business.Analytics;
using EPICommerce.Web.Models.Catalog;
using EPICommerce.Web.Models.ViewModels;

namespace EPICommerce.Web.Controllers
{
    [TemplateDescriptor(Inherited = true)]
    [RequireClientResources]
    public class WineSKUContentController : CommerceControllerBase<WineSKUContent>
    {
        private readonly ICurrentMarket _currentMarket;
        private IWarehouseInventoryService _warehouseInventoryService;
        private LocalizationService _localizationService;
        private ReadOnlyPricingLoader _readOnlyPricingLoader;
        private readonly IPriceDetailService _priceDetailService;

        public WineSKUContentController()
			: this(ServiceLocator.Current.GetInstance<IWarehouseInventoryService>(),
			ServiceLocator.Current.GetInstance<LocalizationService>(),
			ServiceLocator.Current.GetInstance<ReadOnlyPricingLoader>(),
			ServiceLocator.Current.GetInstance<ICurrentMarket>(),
            ServiceLocator.Current.GetInstance<IPriceDetailService>()
			)
		{
		}

        public WineSKUContentController(IWarehouseInventoryService warehouseInventoryService, LocalizationService localizationService, ReadOnlyPricingLoader readOnlyPricingLoader, ICurrentMarket currentMarket, IPriceDetailService priceDetailService)
        {
            _warehouseInventoryService = warehouseInventoryService;
            _localizationService = localizationService;
            _readOnlyPricingLoader = readOnlyPricingLoader;
            _currentMarket = currentMarket;
            _priceDetailService = priceDetailService;
        }


        // GET: WineSKUContent
        public ActionResult Index(WineSKUContent currentContent)
        {
            WineSKUViewModel wineSkuViewModel = new WineSKUViewModel(currentContent);
            wineSkuViewModel.PriceViewModel = GetPriceModel(currentContent);

            TrackAnalytics(wineSkuViewModel);

            wineSkuViewModel.IsSellable = IsSellable(currentContent);
            return View(wineSkuViewModel);
        }

        protected void TrackAnalytics(WineSKUViewModel wineSkuViewModel)
        {
            // Track
            GoogleAnalyticsTracking tracking = new GoogleAnalyticsTracking(ControllerContext.HttpContext);
            tracking.ClearInteractions();

            // Track the main product view
            tracking.ProductAdd(
                wineSkuViewModel.CatalogContent.Code,
                wineSkuViewModel.CatalogContent.DisplayName,
                null,
                wineSkuViewModel.CatalogContent.Facet_Brand,
                null, null, 0,
                (double) wineSkuViewModel.CatalogContent.GetDefaultPriceAmount(_currentMarket.GetCurrentMarket()),
                0
                );

            // TODO: Track related products as impressions

            // Track action as details view
            tracking.Action("detail");
        }
    }
}
