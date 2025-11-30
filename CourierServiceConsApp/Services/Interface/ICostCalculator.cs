using CourierServiceConsApp.Domain;

namespace CourierServiceConsApp.Services.Interface
{
    public interface ICostCalculator
    {
        void CalculateCost(double baseCost, Package package);
    }
}
