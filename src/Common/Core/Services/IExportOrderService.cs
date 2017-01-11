/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using Mediachase.Commerce.Orders;

namespace EPICommerce.Core.Services
{
	public interface IExportOrderService
	{
		string ExportOrder(PurchaseOrder order);
	}
}
