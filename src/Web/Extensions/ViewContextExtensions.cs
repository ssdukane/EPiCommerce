﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Web.Routing.Segments;

namespace EPICommerce.Web.Extensions
{
    public static class ViewContextExtensions
    {
        /// <summary>
        /// Builds a RouteValueDictionary that combines the request route values, the querystring parameters,
        /// and the passed newRouteValues. Values from newRouteValues override request route values and querystring
        /// parameters having the same key.
        /// </summary>
        public static RouteValueDictionary GetCombinedRouteValues(this ViewContext viewContext, object newRouteValues)
        {
            RouteValueDictionary combinedRouteValues = new RouteValueDictionary(viewContext.RouteData.Values);

            NameValueCollection queryString = viewContext.RequestContext.HttpContext.Request.QueryString;
            foreach (string key in queryString.AllKeys.Where(key => key != null))
                combinedRouteValues[key] = queryString[key];

            if (newRouteValues != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(newRouteValues))
                    combinedRouteValues[descriptor.Name] = descriptor.GetValue(newRouteValues);
            }

            return combinedRouteValues;
        }

        public static string GetContentContextMode(this RequestSegmentContext segmentContext)
        {
            if (segmentContext.ContextMode.ToString() != "Default")
                return segmentContext.ContextMode.ToString();

            return segmentContext.QueryString.AllKeys.Contains("epiworkspaceactive") ? "PreviewMode" : segmentContext.ContextMode.ToString();
        }
    }
}
