﻿/*
Commerce Starter Kit for EPiServer

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
	[ContentType(GUID = "0d95fab6-fd3d-4aeb-834e-29fd6b023133",
		DisplayName = "2 Columns",
        Description = "2 columns, each 50% width",
        GroupName = "Content")]
	[SiteImageUrl]
	public class TwoColumnsBlock : SiteBlockData
	{
		[Display(
		Name = "Venstre kolonne - 50%",
		Description = "Dynamic content",
		GroupName = SystemTabNames.Content,
		Order = 10)]
		public virtual ContentArea LeftColumn { get; set; }

		[Display(
		Name = "Høyre kolonne - 50%",
		Description = "Dynamic content",
		GroupName = SystemTabNames.Content,
		Order = 20)]
		public virtual ContentArea RightColumn { get; set; }
	}
}
