﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

namespace EPICommerce.Web.Models.ViewModels
{
	public class VideoViewModel
	{
		public string IframeUrl { get; set; }
		public string CoverImageUrl { get; set; }
		public bool HasCoverImage { get; set; }
		public string IframeId { get; set; }
	    // Local video
        public string Url { get; set; }
	}
}
