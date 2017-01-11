/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System.Collections.Generic;
using EPICommerce.Web.Models.Blocks;
using EPICommerce.Web.Models.PageTypes;

namespace EPICommerce.Web.Models.ViewModels
{
	public class PageListBlockViewModel : PageViewModel<SitePage>
	{

		public PageListBlockViewModel(SitePage currentPage, PageListBlock block)
			: base(currentPage)
        {
			PageListBlock = block;
        }

		public PageListBlock PageListBlock { get; set; }

        public IEnumerable<PageListBlockItemViewModel> Pages { get; set; }

	}
}
