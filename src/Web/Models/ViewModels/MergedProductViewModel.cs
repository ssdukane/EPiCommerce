/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using EPICommerce.Web.Models.Catalog.Base;
using EPICommerce.Web.Models.FindModels;

namespace EPICommerce.Web.Models.ViewModels
{
    public class MergedProductViewModel
    {
        public FindProduct Product { get; set; }
        public ProductBase ProductBase { get; set; }
    }
}
