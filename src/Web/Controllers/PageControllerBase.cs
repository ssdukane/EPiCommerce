﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Logging;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using EPICommerce.Web.Business;
using EPICommerce.Web.Models.PageTypes;
using EPICommerce.Web.Models.ViewModels;

namespace EPICommerce.Web.Controllers
{
	public class PageControllerBase<T> : PageController<T> where T : PageData
	{
		private static Injected<IContentLoader> _contentLoaderService ;
        protected static ILogger _log = LogManager.GetLogger();

		protected IContentLoader ContentLoader
		{
			get { return _contentLoaderService.Service; }
		}
 
		protected T CurrentPage
		{
			get
			{
				return PageContext.Page as T;
			}
		}

		protected override void OnAuthorization(AuthorizationContext filterContext)
		{
			CheckAccess(filterContext);
			base.OnAuthorization(filterContext);
		}

		private void CheckAccess(AuthorizationContext filterContext)
		{
			if (CurrentPage.QueryAccess().HasFlag(AccessLevel.Read))
				return;
			ServeAccessDenied(filterContext);
		}

		private void ServeAccessDenied(AuthorizationContext filterContext)
		{
			_log.Information(
				"AccessDenied",
				new AccessDeniedException(CurrentPage.ContentLink));

			AccessDeniedDelegate accessDenied
				= AccessDeniedHandler.CreateAccessDeniedDelegate(filterContext);
			accessDenied(filterContext);
		}

        protected virtual string GetViewForPageType(PageData currentPage)
        {
            var virtualPath = String.Format("~/Views/{0}/Index.cshtml", currentPage.GetOriginalType().Name);
            if (System.IO.File.Exists(Request.MapPath(virtualPath)) == false)
            {
                virtualPath = "Index";
            }
            return virtualPath;
        }


		public virtual IPageViewModel<PageData> CreatePageViewModel(PageData pageData)
		{
			var activator = new Activator<IPageViewModel<PageData>>();
			var model = activator.Activate(typeof(PageViewModel<>), pageData);
			InitializePageViewModel(model);
			return model;
		}

	    protected void InitializePageViewModel<TViewModel>(TViewModel model) where TViewModel : IPageViewModel<PageData>
		{
			var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
			if (ContentReference.IsNullOrEmpty(ContentReference.StartPage) == false)
			{
                // TODO: Use the Chrome instead
				HomePage startPage = contentLoader.Get<HomePage>(ContentReference.StartPage);

				model.TopLeftMenu = model.TopLeftMenu ?? startPage.TopLeftMenu;
				model.TopRightMenu = model.TopRightMenu ?? startPage.TopRightMenu;
				model.SocialMediaIcons = model.SocialMediaIcons ?? startPage.SocialMediaIcons;
				if (model.CurrentPage != null)
				{
					model.Section = model.Section ?? GetSection(model.CurrentPage.ContentLink);
				}
				else
				{
					model.Section = model.Section ?? GetSection(startPage.ContentLink);
				}
				model.LoginPage = model.LoginPage ?? startPage.Settings.LoginPage;
				model.AccountPage = model.AccountPage ?? startPage.Settings.AccountPage;
			    model.Language = string.IsNullOrEmpty(model.Language) == false ? model.Language : startPage.LanguageBranch;
				model.CheckoutPage = model.CheckoutPage ?? startPage.Settings.CheckoutPage;
			}
		}



        /// <summary>
        /// Returns the closest parent to the start page of the given page.
        /// </summary>
        /// <remarks>
        /// Start Page
        ///     - About Us (This is the section)
        ///         - News
        ///             News 1 (= contentLink parameter)
        /// </remarks>
        /// <param name="contentLink">The content you want to find the section for</param>
        /// <returns>The parent page closes to the start page, or the page referenced by the contentLink itself</returns>
        protected IContent GetSection(ContentReference contentLink)
		{
			var currentContent = ContentLoader.Get<IContent>(contentLink);
			if (currentContent.ParentLink != null && currentContent.ParentLink.CompareToIgnoreWorkID(ContentReference.StartPage))
			{
				return currentContent;
			}

            // Loop upwards until the parent is start page or root
			return ContentLoader.GetAncestors(contentLink)
				.OfType<PageData>()
				.SkipWhile(x => x.ParentLink == null || !x.ParentLink.CompareToIgnoreWorkID(ContentReference.StartPage))
				.FirstOrDefault();
		}

	}
}
