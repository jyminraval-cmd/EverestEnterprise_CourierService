using CourierServiceConsApp.Domain;

namespace CourierServiceConsApp.Services.Interface
{
    public interface IDeliveryScheduler
    {
        void ScheduleDeliveries(List<Package> packages, int numVehicles, double speed, double maxCarriable);
    }
}
