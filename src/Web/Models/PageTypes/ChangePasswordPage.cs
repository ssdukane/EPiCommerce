﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPICommerce.Core.Attributes;

namespace EPICommerce.Web.Models.PageTypes
{
	[ContentType(GUID = "6B7DCD26-0F90-498D-98A3-3EDFDEBF7D87",
		DisplayName = "Change Password Page",
		Description = "The page which allows to change current password.",
        GroupName = WebGlobal.GroupNames.Specialized,
		AvailableInEditMode = false,
		Order = 100)]
    [SiteImageUrl(thumbnail: EditorThumbnail.System)]
	public class ChangePasswordPage : CommerceSampleModulePage
	{
		public virtual XhtmlString UpdatedPassword { get; set; }
	}
}
