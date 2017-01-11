using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPICommerce.Web.Business.Delivery;
using EPICommerce.Web.Services;
using Should;

namespace EPICommerce.Web.Api
{
    static class DeliveryLocationExtensions
    {
        public static void ShouldBeEquivalentOf(this DeliveryLocation location, PostNord.ServicePoint servicePoint)
        {
            location.ShouldNotBeNull();
            location.text.ShouldEqual(servicePoint.Name);
            location.value.Address.ShouldEqual(string.Concat(servicePoint.DeliveryAddress.StreetName, ' ', servicePoint.DeliveryAddress.StreetNumber));
        }
    }
}
