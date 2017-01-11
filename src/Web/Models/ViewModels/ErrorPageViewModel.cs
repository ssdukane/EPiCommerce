/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using EPICommerce.Web.Models.PageTypes;

namespace EPICommerce.Web.Models.ViewModels
{
	public class ErrorPageViewModel : PageViewModel<HomePage>
	{
		public Uri Referer { get; set; }
		public Uri NotFoundUrl { get; set; }

		public bool HasDatabase { get; set; }
	}
}
