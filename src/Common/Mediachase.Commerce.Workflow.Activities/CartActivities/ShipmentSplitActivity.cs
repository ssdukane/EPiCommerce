using System;
using System.Linq;
using System.Workflow.ComponentModel;
using Mediachase.Commerce.Orders;

namespace EPiCode.Commerce.Workflow.Activities
{
    /// <summary>
    /// This activity will look into LineItems defined and will split items based on the address and shipping method id specified 
    /// into different shipments.
    /// </summary>
	public partial class ShipmentSplitActivity : CartActivityBase
	{
        private const string EventsCategory = "Handlers";

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            try
            {
                // Validate the properties at runtime
                this.ValidateRuntime();

                // Process Shipment now
                this.SplitShipments();

                // Retun the closed status indicating that this activity is complete.
                return ActivityExecutionStatus.Closed;
            }
            catch
            {
                // An unhandled exception occured.  Throw it back to the WorkflowRuntime.
                throw;
            }
        }


        /// <summary>
        /// Splits the shipments according to address and shipping method specified.
        /// </summary>
        private void SplitShipments()
        {
            // TODO: 
            foreach (OrderForm form in OrderGroup.OrderForms)
            {
                SplitForm(form);
            }
        }

        private void SplitForm(OrderForm form)
        {
            int index = 0;
            foreach (LineItem item in form.LineItems)
            {
                Shipment itemShipment = null;
                string key = index.ToString();

                // Find appropriate shipment for item
                foreach (Shipment shipment in form.Shipments)
                {
                    if ((string.IsNullOrEmpty(shipment.ShippingAddressId) && string.IsNullOrEmpty(item.ShippingAddressId)) ||
                        OrderGroup.OrderAddresses.Cast<OrderAddress>().Any(x => x.Name.Equals(shipment.ShippingAddressId)))
                    {
                        itemShipment = shipment;
                    }
                    else
                    {
                        if (shipment.LineItemIndexes.Contains(key))
                            shipment.RemoveLineItemIndex(Convert.ToInt32(key));
                    }
                }

                // did we find any shipment?
                if (itemShipment == null)
                {
                    itemShipment = form.Shipments.AddNew();
                    itemShipment.ShippingAddressId = item.ShippingAddressId;
                    itemShipment.ShippingMethodId = item.ShippingMethodId;
                    itemShipment.ShippingMethodName = item.ShippingMethodName;
                }

                // As long as this is only used in conjunction with GetFulfillmentCenterActivity, this should be OK...
                if (String.IsNullOrEmpty(itemShipment.WarehouseCode))
                {
                    itemShipment.WarehouseCode = item.WarehouseCode; 
                }

                // Add item to the shipment
                //if (item.LineItemId == 0)
                //    throw new ArgumentNullException("LineItemId = 0");

                if (itemShipment.LineItemIndexes.Contains(key))
                    itemShipment.RemoveLineItemIndex(key);

                //if (!itemShipment.LineItemIds.Contains(key))
                itemShipment.AddLineItemIndex(index, item.Quantity);

                index++;
            }
        }

		public ShipmentSplitActivity()
			:base()
		{
			InitializeComponent();
		}
	}
}
