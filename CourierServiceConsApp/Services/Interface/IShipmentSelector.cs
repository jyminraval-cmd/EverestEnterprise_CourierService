using CourierServiceConsApp.Domain;

namespace CourierServiceConsApp.Services.Interface
{
    public interface IShipmentSelector
    {
        List<Package> PickBestShipment(List<Package> remaining, double capacity);
    }
}
