﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using Mediachase.Commerce;

namespace EPICommerce.Web.Models.ViewModels
{
    public interface IProductListViewModelInitializer
    {
        ProductListViewModel Populate(IMarket getCurrentMarket);
    }
}
