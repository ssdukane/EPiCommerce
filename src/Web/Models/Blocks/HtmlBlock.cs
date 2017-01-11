﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPICommerce.Core.Attributes;

namespace EPICommerce.Web.Models.Blocks
{
	[ContentType(
        GUID = "c26c3249-e6ec-4850-8bc5-cd70e4f547b2",
        GroupName="Content")]
	[SiteImageUrl]
	public class HtmlBlock : SiteBlockData
	{
		[Display(
			GroupName = SystemTabNames.Content,
			Order = 10)]
		[CultureSpecific]
		public virtual XhtmlString Html { get; set; }
	}
}
