/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Framework.DataAnnotations;
using EPiServer.GoogleAnalytics.Helpers;
using EPiServer.Security;
using EPiServer.Web.Mvc;
using Mediachase.Commerce.Customers;
using EPICommerce.Web.Business.Analytics;
using EPICommerce.Web.Models.PageTypes;
using EPICommerce.Web.Models.ViewModels;

namespace EPICommerce.Web.Controllers
{
    [TemplateDescriptor()]
    public class ArticleWithSidebarPageController : PageControllerBase<ArticleWithSidebarPage>
    {
		private readonly IContentLoader _contentLoader;

        public ArticleWithSidebarPageController(IContentLoader contentLoader)
        {            
			_contentLoader = contentLoader;		    
        }

        public ViewResult Index(PageData currentPage)
        {
            var viewPath = GetViewForPageType(currentPage);

            var model = CreatePageViewModel(currentPage);

            return View(viewPath, model);
        }
    }
}
