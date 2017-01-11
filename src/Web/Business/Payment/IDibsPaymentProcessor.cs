using System.Security.Principal;
using EPICommerce.Core.PaymentProviders.DIBS;

namespace EPICommerce.Web.Business.Payment
{
    public interface IDibsPaymentProcessor
    {
        DibsPaymentProcessingResult ProcessPaymentResult(DibsPaymentResult result, IIdentity identity);
    }
}