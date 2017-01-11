using EPICommerce.Core.Objects.SharedViewModels;

namespace EPICommerce.Web.Business.Payment
{
    public interface IStockUpdater
    {
        void AdjustStocks(PurchaseOrderModel order);
    }
}