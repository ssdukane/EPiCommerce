/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using EPICommerce.Web.Models.PageTypes;

namespace EPICommerce.Web.Models.ViewModels
{
    public class SearchPageViewModel : PageViewModel<SearchPage>
    {
        public SearchPageViewModel(SearchPage currentPage)
            : base(currentPage)
        {
            
        }
        public int NumberOfProductsToShow { get; set; }
        public int NumberOfArticlesToShow { get; set; }
        public string SearchedQuery { get; set; }
    }
}
