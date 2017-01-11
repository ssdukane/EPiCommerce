﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using EPiServer.DataAnnotations;
using EPICommerce.Core.Attributes;

namespace EPICommerce.Web.Models.PageTypes
{
    [ContentType(GUID = "BAE8C7EE-AA11-4884-8ECC-325AB02B9E8E",
        DisplayName = "Cart Page",
        GroupName = WebGlobal.GroupNames.Commerce,
        Order = 100,
		AvailableInEditMode = false,
		Description = "")]
    [SiteImageUrl(thumbnail: EditorThumbnail.Commerce)]
    public class CartSimpleModulePage : CommerceSampleModulePage
    {
        
    }
}
