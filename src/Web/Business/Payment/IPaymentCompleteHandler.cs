using System.Security.Principal;
using EPICommerce.Core.Objects.SharedViewModels;

namespace EPICommerce.Web.Business.Payment
{
    public interface IPaymentCompleteHandler
    {
        void ProcessCompletedPayment(PurchaseOrderModel orderModel, IIdentity identity);
    }
}