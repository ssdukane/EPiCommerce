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
	[ContentType(GUID = "2b0b9fb5-967a-46d1-80a5-4c23299602ee",
		DisplayName = "1 + 2 Columns",
		Description = "2 columns - 33% + 66%",
		GroupName = "Content")]
	[SiteImageUrl]
	public class OneTwoColumnsBlock : SiteBlockData
	{
		[Display(
		Name = "Venstre kolonne - 33%",
		Description = "Dynamic content",
		GroupName = SystemTabNames.Content,
		Order = 10)]
		public virtual ContentArea LeftColumn { get; set; }

		[Display(
		Name = "Høyre kolonne - 66%",
		Description = "Dynamic content",
		GroupName = SystemTabNames.Content,
		Order = 20)]
		public virtual ContentArea RightColumn { get; set; }
	}
}
