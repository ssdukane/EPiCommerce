﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Find;
using EPiServer.Find.Api;
using EPiServer.Find.Framework;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;
using EPICommerce.Web.Models.FindModels;
using EPICommerce.Web.Models.ViewModels;
using EPICommerce.Web.Services;


namespace EPICommerce.Web.Models.Blocks.Base
{
    public abstract class FindBaseBlockType : SiteBlockData
    {
        private int _startingIndex = 0;

        [Display(Order = 5,
         Name = "Heading")]
        [CultureSpecific]
        public virtual string Heading { get; set; }

        [Display(Name = "Number of Results",
                 Description = "The number of products to show in the list. Default is 6.",
                 Order = 9)]
        [CultureSpecific]
        public virtual int ResultsPerPage { get; set; }

        [Display(Order = 11,
                 Name = "Min Price",
                 Description = "The minimum price in the current market currency")]
        [CultureSpecific]
        public virtual int MinPrice { get; set; }

        [Display(Order = 13,
                 Name = "Max Price",
                 Description = "The maximum price in the current market currency")]
        [CultureSpecific]
        public virtual int MaxPrice { get; set; }


        public void SetIndex(int index)
        {
            _startingIndex = index;
        }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            ResultsPerPage = 6;
        }

        public virtual IClient GetSearchClient()
        {
            return ServiceLocator.Current.GetInstance<IClient>();
        }

        public virtual SearchResults<FindProduct> GetResults(string language)
        {

            int numberofPages = 6;
            if (ResultsPerPage> 0)
                numberofPages = ResultsPerPage;
            var query = SearchClient.Instance.Search<FindProduct>();

            // Let Model apply filters
            query = ApplyFilters(query);

            query = query.Filter(x => x.DefaultPriceAmount.InRange(MinPrice, MaxPrice != 0 ? MaxPrice : int.MaxValue));
            
            if(string.IsNullOrEmpty(language))
                language = ContentLanguage.PreferredCulture.Name;

            query = query.Filter((x => x.Language.MatchCaseInsensitive(language)));
            return query.Take(numberofPages).StaticallyCacheFor(TimeSpan.FromMinutes(1)).GetResult();
        }

        /// <summary>
        /// Let derived classes apply additional filters that only they know
        /// as the data being filtered on is part of the model
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        protected abstract ITypeSearch<FindProduct> ApplyFilters(ITypeSearch<FindProduct> query);


        public virtual List<ProductListViewModel> GetSearchResults(string language)
        {
            IContentLoader loader = ServiceLocator.Current.GetInstance<IContentLoader>();
            ProductService productService = ServiceLocator.Current.GetInstance<ProductService>();
            ReferenceConverter refConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();

            SearchResults<FindProduct> results = GetResults(language);
            
            List<ProductListViewModel> searchResult = new List<ProductListViewModel>();
            foreach (SearchHit<FindProduct> searchHit in results.Hits)
            {
                ContentReference contentLink = refConverter.GetContentLink(searchHit.Document.Id, CatalogContentType.CatalogEntry, 0);
                var content = loader.Get<IContentData>(contentLink);

                IProductListViewModelInitializer modelInitializer = content as IProductListViewModelInitializer;
                if (modelInitializer != null)
                {
                    searchResult.Add(productService.GetProductListViewModel(modelInitializer));
                }
            }

            return searchResult;

        }

    }
}
