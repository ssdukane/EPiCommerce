﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using EPICommerce.Core.PaymentProviders.DIBS;
using EPICommerce.Web.Models.PageTypes.Payment;

namespace EPICommerce.Web.Models.ViewModels.Payment
{
    public class CancelPaymentViewModel : PageViewModel<DibsPaymentPage>
    {
        public CancelPaymentViewModel(DibsPaymentPage currentPage, DibsPaymentResult result)
            : base(currentPage)
        {
            
        }
    }
}
