using EPICommerce.Web.Models.ViewModels;

namespace EPICommerce.Web.Business.ClientTracking
{
    public interface IGoogleAnalyticsTracker
    {
        void TrackAfterPayment(ReceiptViewModel model);
        void TrackPromotionImpression(string id, string name, string bannerName);
        string GetPromotionClickScript(string id, string name, string bannerName);
    }
}