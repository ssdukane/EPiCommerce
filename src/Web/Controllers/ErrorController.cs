﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BVNetwork.Bvn.FileNotFound.Logging;
using BVNetwork.Bvn.FileNotFound.Upgrade;
using BVNetwork.FileNotFound.Configuration;
using BVNetwork.FileNotFound.Redirects;
using EPiServer;
using EPiServer.Core;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using log4net;
using EPICommerce.Web.Models.PageTypes;
using EPICommerce.Web.Models.ViewModels;
using LogManager = log4net.LogManager;

namespace EPICommerce.Web.Controllers
{
    public class ErrorController : PageControllerBase<PageData>
    {
        // GET: Error404
        public ActionResult Error404()
        {
			ErrorPageViewModel model = GetViewModel();

			model.Referer = HttpContext.Request.UrlReferrer;
			model.NotFoundUrl = GetUrlNotFound(HttpContext);

			HandleOnLoad(HttpContext, model.NotFoundUrl, model.Referer);

			return View("Error404", model);
        }

		private static ErrorPageViewModel GetViewModel()
		{
			ErrorPageViewModel model;
			try
			{
				var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();				

				model = CreateErrorPageViewModel(contentLoader.Get<HomePage>(ContentReference.StartPage));
				model.HasDatabase = true;
			}
			catch (Exception)
			{
				model = new ErrorPageViewModel();
			}
			return model;
		}

        private static ErrorPageViewModel CreateErrorPageViewModel(HomePage pageData)
        {
            var model = new ErrorPageViewModel();
            model.CurrentPage = pageData;            
            return (ErrorPageViewModel)model;
        }

		public ActionResult Error500(Exception exception = null)
		{
			ErrorPageViewModel model = GetViewModel();

			if (exception != null && !(exception is OperationCanceledException || exception is TaskCanceledException) && (
				HttpContext == null || (HttpContext != null && !HttpContext.Request.Url.ToString().EndsWith("/find_v2/"))))
			{
				_log.Error(exception.Message,exception);
			}



			return View("Error500", model);
		}

		/// <summary>
		/// Copied from BVNetwork.FileNotFound.NotFoundPageUtil.HandleOnLoad(Page page, Uri urlNotFound, string referer)
		/// </summary>
		/// <param name="context"></param>
		/// <param name="urlNotFound"></param>
		/// <param name="referer"></param>
		protected void HandleOnLoad(HttpContextBase context, Uri urlNotFound, Uri referer)
		{
            // TODO: This copy should be removed. The 404 should be extended to work without
            // a Page object.

			if (_log.IsDebugEnabled())
			{
				_log.Debug("Trying to handle 404 for \"{0}\" (Referrer: \"{1}\")", urlNotFound, referer);
			}
			CustomRedirectHandler current = CustomRedirectHandler.Current;
			CustomRedirect customRedirect = current.CustomRedirects.Find(new Uri(urlNotFound.AbsoluteUri));
			string str = HttpUtility.HtmlEncode(urlNotFound.PathAndQuery);
			if (customRedirect == null)
			{
                // Check relative uri
                customRedirect = current.CustomRedirects.Find(urlNotFound);
			}
			if (customRedirect != null)
			{
				if (customRedirect.State.Equals(0) && string.Compare(customRedirect.NewUrl, str, StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					_log.Information(string.Format("404 Custom Redirect: To: '{0}' (from: '{1}')", customRedirect.NewUrl, str));
					context.Response.Clear();
					context.Response.StatusCode = 301;
					context.Response.StatusDescription = "Moved Permanently";
					context.Response.RedirectLocation = customRedirect.NewUrl;
					context.Response.End();
					return;
				}
			}
			else if (Configuration.Logging == LoggerMode.On && Upgrader.Valid)
			{
				Logger.LogRequest(str, referer == null ? string.Empty : referer.ToString());
			}
			context.Response.TrySkipIisCustomErrors = true;
			context.Response.StatusCode = 404;
			context.Response.Status = "404 File not found";
		}


		public static Uri GetUrlNotFound(HttpContextBase context)
		{
			Uri uri = null;
			string item = context.Request.ServerVariables["QUERY_STRING"];
			if (item != null && item.StartsWith("404;"))
			{
				char[] chrArray = new char[] { ';' };
				uri = new Uri(item.Split(chrArray)[1]);
			}
			if (uri == null && item.StartsWith("aspxerrorpath="))
			{
				string[] strArrays = item.Split(new char[] { '=' });
				uri = new Uri(string.Concat(context.Request.Url.GetLeftPart(UriPartial.Authority), HttpUtility.UrlDecode(strArrays[1])));
			}
			return uri;
		}
    }
}
