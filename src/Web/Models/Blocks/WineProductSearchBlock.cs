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
using System.Web;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Find;
using EPiServer.Shell.ObjectEditing;
using EPICommerce.Core.Attributes;
using EPICommerce.Web.EditorDescriptors;
using EPICommerce.Web.EditorDescriptors.Attributes;
using EPICommerce.Web.Models.Blocks.Base;
using EPICommerce.Web.Models.Catalog;
using EPICommerce.Web.Models.CustomProperties;
using EPICommerce.Web.Models.FindModels;

namespace EPICommerce.Web.Models.Blocks
{
    [ContentType(
        DisplayName = "Configurable Wine List",
        GUID = "C14C3799-AC95-46D3-9CED-B3C5490FA005",
        Description = "Configurable search block for wines, allows filtering on price, regions and brands.",
        GroupName = "Commerce")]
    [SiteImageUrl(thumbnail: EditorThumbnail.Commerce)]
    public class WineProductSearchBlock : FindBaseBlockType
    {

        [Display(Order = 70, 
                 Name = "Countries",
                 Description = "List Wines from one or more countries")]
        [BackingType(typeof(PropertyTagList))]
        [UIHint("TagSelector")]
        [TagSelectorApi("/api/tag/GetCountries")]
        [CultureSpecific]
        public virtual List<string> CountryList { get; set; }

        [Display(Order = 90, 
                 Name = "Brands",
                 Description = "List only products from given brands")]
        [BackingType(typeof(PropertyTagList))]
        [UIHint("TagSelector")]
        [TagSelectorApi("/api/tag/GetBrands")]
        [CultureSpecific]
        public virtual List<string> ProductBrands { get; set; }


        [Display(Name = "Priority Products", 
            Description = "Products to put first in the list.", 
            Order = 200)]
        [AllowedTypes(typeof(WineSKUContent))]
        [CultureSpecific]
        public virtual ContentArea Products { get; set; }


        public FilterBuilder<FindProduct> GetWineTypeFilter(IEnumerable<string> wineTypes)
        {
            var filter = GetSearchClient().BuildFilter<FindProduct>();
            return wineTypes.Aggregate(filter, (current, wineType) => current.Or(x => x.CategoryName.MatchCaseInsensitive(wineType)));
        }

        public FilterBuilder<FindProduct> GetBrandFilter(IEnumerable<string> brands)
        {
            var filter = GetSearchClient().BuildFilter<FindProduct>();
            return brands.Aggregate(filter, (current, brand) => current.Or(x => x.Brand.MatchCaseInsensitive(brand)));
        }

        public FilterBuilder<FindProduct> GetCountryFilter(IEnumerable<string> countryList)
        {
            var filter = GetSearchClient().BuildFilter<FindProduct>();
            return countryList.Aggregate(filter, (current, country) => current.Or(x => x.Country.MatchCaseInsensitive(country)));
        }


        /// <summary>
        /// Applies the filters applicable to this given model.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        protected override ITypeSearch<FindProduct> ApplyFilters(ITypeSearch<FindProduct> query)
        {
            if (ProductBrands != null && ProductBrands.Any())
                query = query.Filter(x => GetBrandFilter(ProductBrands));
            
            //if (category != null && category.Any())
            //    query = query.Filter(x => GetWineTypeFilter(category));

            if (CountryList != null && CountryList.Any())
                query = query.Filter(x => GetCountryFilter(CountryList));
            
            return query;
        }
    }

 
}
