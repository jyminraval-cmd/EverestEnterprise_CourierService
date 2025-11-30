using CourierServiceConsApp.Domain;
using CourierServiceConsApp.Services.Interface;

namespace CourierServiceConsApp.Services.Implementations
{
    public class CostCalculator : ICostCalculator
    {
        private readonly IOfferService _offerService;

        public CostCalculator(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public void CalculateCost(double baseCost, Package package)
        {
            double deliveryCost = baseCost + (package.Weight * 10) + (package.Distance * 5);
            package.DeliveryCostBase = deliveryCost;

            var offer = _offerService.GetOffer(package.OfferCode);
            double discount = 0;

            if (offer != null && offer.IsApplicable(package))
                discount = Math.Round(deliveryCost * offer.DiscountPercent / 100, 2);

            package.Discount = discount;
            package.TotalCost = Math.Round(deliveryCost - discount, 2);
        }
    }
}
