/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web.Mvc;
using EPICommerce.Web.Business;
using EPICommerce.Web.Models.ViewModels;

namespace EPICommerce.Web.Controllers
{
    [TemplateDescriptor(Inherited = true)]
    [RequireClientResources]
    public class VariationContentController : CommerceControllerBase<VariationContent>
    {    
        public VariationContentController()
        {            
        }

        public ViewResult Index(VariationContent currentContent, PageData currentPage)
        {
            var virtualPath = String.Format("~/Views/{0}/Index.cshtml", currentContent.GetOriginalType().Name);
            if (!System.IO.File.Exists(Request.MapPath(virtualPath)))
            {
                virtualPath = String.Format("~/Views/{0}/Index.cshtml", typeof(VariationContent).Name);
            }

            var model = CreateVariationViewModel<VariationContent, PageData>(currentContent);
            return View(virtualPath, model);
        }


        public IVariationViewModel<TContent> CreateVariationViewModel<TContent, TPage>(TContent variationContent)
            where TContent : VariationContent
            where TPage : PageData
        {
            var activator = new Activator<IVariationViewModel<TContent>>();
            var model = activator.Activate(typeof(VariationViewModel<TContent>), variationContent);
            InitializeVariationViewModel(model);
            return model;
        }
    }
}
