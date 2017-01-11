using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPICommerce.Core.Objects.SharedViewModels
{
    public class ShipmentModel
    {
        public decimal ShippingDiscountAmount { get; set; }

        public string ShipmentTrackingNumber { get; set; }

        public DiscountModel[] Discounts { get; set; }
    }
}
