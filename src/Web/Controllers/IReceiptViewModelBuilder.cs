using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPICommerce.Web.Business.Payment;
using EPICommerce.Web.Models.ViewModels;

namespace EPICommerce.Web.Controllers
{
    public interface IReceiptViewModelBuilder
    {
        ReceiptViewModel BuildFor(DibsPaymentProcessingResult processingResult);
    }
}