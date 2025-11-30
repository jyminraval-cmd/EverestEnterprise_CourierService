using CourierServiceConsApp.Domain;
using CourierServiceConsApp.Infrastructure;
using CourierServiceConsApp.Services.Interface;

namespace CourierServiceConsApp.Services.Implementations
{
    public class OfferService : IOfferService
    {
        private readonly OfferRepository _repository;

        public OfferService(OfferRepository repository)
        {
            _repository = repository;
        }

        public Offer? GetOffer(string code)
        {
            return _repository.GetOffers().FirstOrDefault(o =>
                o.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        public List<Offer> GetAllOffers()
        {
            return _repository.GetOffers();
        }

        public void AddOffer(Offer offer)
        {
            _repository.AddOffer(offer);
        }

        public bool RemoveOffer(string code)
        {
            return _repository.RemoveOffer(code);
        }
    }
}
