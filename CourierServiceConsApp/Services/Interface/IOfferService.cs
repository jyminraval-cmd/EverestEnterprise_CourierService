using CourierServiceConsApp.Domain;

namespace CourierServiceConsApp.Services.Interface
{
    public interface IOfferService
    {
        Offer? GetOffer(string code);
        List<Offer> GetAllOffers();
        void AddOffer(Offer offer);
        bool RemoveOffer(string code);
    }
}
