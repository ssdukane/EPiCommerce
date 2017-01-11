﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System.Collections.Generic;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Managers;
using Mediachase.Commerce.Website.Helpers;
using EPICommerce.Web.Models.PageTypes;

namespace EPICommerce.Web.Models.ViewModels
{
    public class CartModel : PageViewModel<CartSimpleModulePage>
    {
        

        public CartModel(CartSimpleModulePage currentPage )
            : base(currentPage)
        {
             
        }

        /// <summary>
        /// Gets the line items for the current cart.
        /// </summary>
        /// <remarks>
        /// Important! This property will run the CartValidate workflow
        /// </remarks>
        /// <value>
        /// The line items for the current default cart.
        /// </value>
        public IEnumerable<LineItem> LineItems
        {
            get
            {
                var cartHelper = new CartHelper(Cart.DefaultName);
                bool isEmpty = cartHelper.IsEmpty;

                // Make sure to check that prices has not changed
                if (!isEmpty)
                {
                    // TODO: Move provider id into custom carthelper so we do not forget
                    cartHelper.Cart.ProviderId = "FrontEnd";
                    cartHelper.RunWorkflow(OrderGroupWorkflowManager.CartValidateWorkflowName);

                    // If cart is empty, remove it from the database
                    isEmpty = cartHelper.IsEmpty;
                    if (isEmpty)
                    {
                        cartHelper.Delete();
                    }

                    cartHelper.Cart.AcceptChanges();
                }

                return cartHelper.LineItems;
            }
        }
    }
}
