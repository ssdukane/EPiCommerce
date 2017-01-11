﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using EPiServer.Shell;
using EPICommerce.Web.Models.Catalog;

namespace EPICommerce.Web.Business.UIDescriptor
{
    [UIDescriptorRegistration]
    public class CatalogContentUiDescriptor : UIDescriptor<FashionProductContent>
    {
        public CatalogContentUiDescriptor()
            : base()
        {
            DefaultView = CmsViewNames.OnPageEditView;

        }
    }


    [UIDescriptorRegistration]
    public class WineContentUiDescriptor : UIDescriptor<WineSKUContent>
    {
        public WineContentUiDescriptor()
            : base()
        {
            DefaultView = CmsViewNames.OnPageEditView;

        }
    }


}
