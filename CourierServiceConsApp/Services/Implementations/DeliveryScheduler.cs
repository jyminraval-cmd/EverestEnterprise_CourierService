using CourierServiceConsApp.Domain;
using CourierServiceConsApp.Services.Interface;

namespace CourierServiceConsApp.Services.Implementations
{
    public class DeliveryScheduler : IDeliveryScheduler
    {
        private readonly IShipmentSelector _shipmentSelector;

        public DeliveryScheduler(IShipmentSelector shipmentSelector)
        {
            _shipmentSelector = shipmentSelector;
        }

        public void ScheduleDeliveries(List<Package> packages, int numVehicles, double speed, double maxCarriable)
        {
            var vehicles = new List<Vehicle>();

            for (int i = 0; i < numVehicles; i++)
                vehicles.Add(new Vehicle { Id = i + 1, MaxCarryWeight = maxCarriable, Speed = speed });

            var remaining = packages.ToList();

            while (remaining.Any())
            {
                var nextTime = vehicles.Min(v => v.AvailableAt);
                var available = vehicles.Where(v => v.AvailableAt == nextTime).ToList();

                foreach (var vehicle in available)
                {
                    if (!remaining.Any()) break;

                    var shipment = _shipmentSelector.PickBestShipment(remaining, vehicle.MaxCarryWeight);
                    if (!shipment.Any()) break;

                    double maxDist = shipment.Max(p => p.Distance);
                    double travelOneWay = maxDist / vehicle.Speed;

                    foreach (var pkg in shipment)
                    {
                        pkg.EstimatedDeliveryTime = Math.Round(vehicle.AvailableAt + (pkg.Distance / vehicle.Speed), 2);
                    }

                    vehicle.AvailableAt += 2 * travelOneWay;

                    foreach (var pkg in shipment) remaining.Remove(pkg);
                }
            }
        }

        public List<List<Package>> CreateShipmentsForTesting(List<Package> packages, double maxWeight)
        {
            return _shipmentSelector.CreateShipments(packages, maxWeight);
        }
    }
}
