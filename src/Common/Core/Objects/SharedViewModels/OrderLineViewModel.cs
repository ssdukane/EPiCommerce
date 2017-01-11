﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using EPICommerce.Core.Extensions;

namespace EPICommerce.Core.Objects.SharedViewModels
{
	public class OrderLineViewModel
	{
        public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Color { get; set; }
		public string Size { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public string ArticleNumber { get; set; }

		public OrderLineViewModel() { }
		public OrderLineViewModel(LineItemModel item)
		{
		    Code = item.CatalogEntryId;
			Name = item.DisplayName;
			Description = item.Description;
			Price = item.ExtendedPrice;
			Discount = item.LineItemDiscountAmount + item.OrderLevelDiscountAmount;
			Quantity = item.Quantity;
			Size = item.Size;
			Color = item.Color;
			ArticleNumber = item.ArticleNumber;
		}
	}
}
