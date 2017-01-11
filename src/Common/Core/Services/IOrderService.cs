﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Collections.Generic;
using System.Security.Principal;
using EPICommerce.Core.Objects.SharedViewModels;

namespace EPICommerce.Core.Services
{
    public interface IOrderService
    {
        PurchaseOrderModel GetOrderByTrackingNumber(string trackingNumber);
        IEnumerable<PurchaseOrderModel> GetOrdersByUserId(Guid customerId);
        void FinalizeOrder(string orderTrackingNumber, IIdentity identity);
    }
}
