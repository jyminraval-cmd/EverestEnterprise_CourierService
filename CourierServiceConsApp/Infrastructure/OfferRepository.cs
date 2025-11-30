using CourierServiceConsApp.Domain;

namespace CourierServiceConsApp.Infrastructure
{
    public class OfferRepository
    {
        private readonly List<Offer> _offers;

        public OfferRepository()
        {
            _offers = new List<Offer>
            {
                new Offer { Code = "OFR001", DiscountPercent = 10, MinWeight = 70, MaxWeight = 200, MinDistance = 0, MaxDistance = 200 },
                new Offer { Code = "OFR002", DiscountPercent = 7,  MinWeight = 100, MaxWeight = 250, MinDistance = 50, MaxDistance = 150 },
                new Offer { Code = "OFR003", DiscountPercent = 5,  MinWeight = 10,  MaxWeight = 150, MinDistance = 50, MaxDistance = 250 },
            };
        }

        public List<Offer> GetOffers() => _offers;

        public void AddOffer(Offer offer)
        {
            _offers.Add(offer);
        }

        public bool RemoveOffer(string code)
        {
            var offer = _offers.FirstOrDefault(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (offer != null)
            {
                _offers.Remove(offer);
                return true;
            }
            return false;
        }
    }
}
