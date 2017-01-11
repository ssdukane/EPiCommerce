﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using Mediachase.Commerce;
using Mediachase.Commerce.Catalog.Objects;

namespace EPICommerce.Core.Models
{
    public class PriceAndMarket
    {
        public string Price { get; set; }
        public string MarkedId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string PriceTypeId { get; set; }
        public string PriceCode { get; set; }
    }
}
