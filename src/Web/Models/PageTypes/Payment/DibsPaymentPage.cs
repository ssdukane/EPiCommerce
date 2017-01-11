﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPICommerce.Core.Attributes;

namespace EPICommerce.Web.Models.PageTypes.Payment
{
	[ContentType(DisplayName = "DibsPaymentPage", 
		GUID = "171d2834-df08-4cb1-8921-79ea32e185f9", 
		Description = "", 
		AvailableInEditMode = false,
		GroupName = "Payment")]
    [SiteImageUrl]
    public class DibsPaymentPage : BasePaymentPage
	{
        [Display(
                    Name = "Cancel Page title",
                    Description = "Cancel Page ",
                    GroupName = "Cancel Page",
                    Order = 20)]
        [CultureSpecific]
        public virtual string CancelPageTitle { get; set; }

        [Display(
            Name = "Cancel Body",
            Description = "The content to show a user that has cancelled the payment",
            GroupName = "Cancel Page",
            Order = 40)]
        [CultureSpecific]
        public virtual XhtmlString CancelBodyText { get; set; }


	}
}
