﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework;
using EPiServer.Web.Routing;
using EPICommerce.Web.Models.FindModels;

namespace EPICommerce.Web.Api
{
    public class AutocompleteSearchController : BaseApiController
    {
        public class AutoCompleteObject
        {
            public string Name { get; set; }
            public string Image { get; set; }
            public string Url { get; set; }
            public string ResultType { get; set; }
            public string GroupHeading { get; set; }
            public string Price { get; set; }
			public string DiscountedPrice { get; set; }
        }


        [System.Web.Http.HttpGet]
        public object Search(string term)
        {
            try
            {
                SetLanguage();
                string language = Language;
                var findLanguage = ShoppingController.GetLanguage(language);
                IEnumerable<SearchResults<AutoCompleteObject>> results =
                    SearchClient.Instance.MultiSearch<AutoCompleteObject>()

                        .Search<FindProduct, AutoCompleteObject>(findLanguage, x => x
                            .For(term)
                            .InFields(a => a.Name, a => a.MainCategoryName, a => string.Join(",", a.Color),
                                a => a.DisplayName, a => a.Brand, a => a.Country, a => string.Join(",", a.GrapeMixList),
                                a => a.Fit, a => a.Description.ToString(), a => string.Join(",", a.ParentCategoryName))
                            .InAllField()
                            .Filter(z => z.ShowInList.Match(true))
                            .Filter(a => a.Language.Match(language))
                            .Filter(z => !z.DefaultImageUrl.Match("/siteassets/system/no-image.png"))
                            // remove products without image
                            .Take(3)
                            .Select(y => new AutoCompleteObject
                            {
                                ResultType = "products",
                                GroupHeading = "Produkter",
                                Name = y.DisplayName,
                                Image = y.DefaultImageUrl,
                                Price = y.DefaultPrice,
                                DiscountedPrice = y.DiscountedPrice,
                                Url = y.ProductUrl
                            }))

                        .Search<PageData, AutoCompleteObject>(findLanguage, x => x
                            .For(term)
                            //.Track()
                            .Filter(a => a.Language.Name.Match(language))
                            .FilterForVisitor()
                            .Take(5)
                            .Select(y => new AutoCompleteObject
                            {
                                ResultType = "pages",
                                GroupHeading = "Annet",
                                Name = y.PageName,
                                Image = "",
                                Url = UrlResolver.Current.GetUrl(y.PageLink)
                            }))

                        .GetResult();

                List<AutoCompleteObject> allResList = new List<AutoCompleteObject>();
                foreach (var result1 in results)
                {
                    allResList.AddRange(result1);
                }
                var result = new
                {
                    allResult = allResList
                };
                return result;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (ServiceException)
            {
                return null;
            }
        }

        
    }
}
