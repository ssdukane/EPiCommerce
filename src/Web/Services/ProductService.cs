﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.DataAccess;
using EPiServer.Web.Routing;
using Mediachase.Commerce;
using EPICommerce.Core.Extensions;
using EPICommerce.Web.Models.Catalog;
using EPICommerce.Web.Models.ViewModels;

namespace EPICommerce.Web.Services
{
    public class ProductService
    {
        private IContentLoader _contentLoader;
        private UrlResolver _urlResolver;
        private ILinksRepository _linksRepository;
        private readonly ICurrentMarket _currentMarket;

        public ProductService(IContentLoader contentLoader, UrlResolver urlResolver, ILinksRepository linksRepository, ICurrentMarket currentMarket)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
            _linksRepository = linksRepository;
            _currentMarket = currentMarket;
        }

        //public ProductListViewModel GetProductListViewModel(WineSKUContent wineSkuContent)
        //{
        //    IProductListViewModelInitializer modelInitializer = wineSkuContent as IProductListViewModelInitializer;
        //    if (modelInitializer != null)
        //        return modelInitializer.Populate();
        //    return null;
        //}

        public ProductListViewModel GetProductListViewModel(IProductListViewModelInitializer productContent)
        {
            if (productContent != null)
                return productContent.Populate(_currentMarket.GetCurrentMarket());
            return null;
        }
       
    }
}
