using CourierServiceConsApp.Domain;
using CourierServiceConsApp.Infrastructure;
using CourierServiceConsApp.Services.Implementations;
using Xunit;

namespace CourierService.Tests
{
    public class DeliverySchedulerTests
    {
        private readonly DeliveryScheduler _scheduler;

        public DeliverySchedulerTests()
        {
            var repo = new OfferRepository();
            var offerService = new OfferService(repo);

            // Scheduler depends on ShipmentSelector internally
            _scheduler = new DeliveryScheduler(new ShipmentSelector());
        }

        [Fact]
        public void ScheduleDeliveries_ShouldAssignDeliveryTime_ForAllPackages()
        {
            // Arrange
            var packages = new List<Package>()
            {
                new Package("PKG1", 50, 30, "OFR001"),
                new Package("PKG2", 75, 125, "OFR002"),
                new Package("PKG3", 175, 100, "OFR003")
            };

            int numVehicles = 2;
            double vehicleSpeed = 70;
            double maxCarriable = 200;

            // Act
            _scheduler.ScheduleDeliveries(packages, numVehicles, vehicleSpeed, maxCarriable);

            // Assert: All packages must have a delivery time
            Assert.All(packages, pkg => Assert.True(pkg.EstimatedDeliveryTime > 0));
        }

        [Fact]
        public void ScheduleDeliveries_ShouldNotExceedMaxVehicleWeight()
        {
            // Arrange
            var packages = new List<Package>()
            {
                new Package("PKG1", 150, 30, "NA"),
                new Package("PKG2", 100, 20, "NA"),
                new Package("PKG3", 60, 50, "NA")
            };

            double vehicleSpeed = 50;
            double maxWeight = 200;

            // Act
            var shipments = _scheduler.CreateShipmentsForTesting(packages, maxWeight);

            // Assert: Every shipment must be <= maxWeight
            foreach (var ship in shipments)
            {
                double total = ship.Sum(p => p.Weight);
                Assert.True(total <= maxWeight);
            }
        }

        [Fact]
        public void ScheduleDeliveries_ShouldPrioritizeHeaviestPackages()
        {
            // Arrange
            var packages = new List<Package>()
            {
                new Package("A", 30, 20, "NA"),
                new Package("B", 90, 40, "NA"),
                new Package("C", 100, 50, "NA")
            };

            double maxWeight = 200;

            // Act
            var shipments = _scheduler.CreateShipmentsForTesting(packages, maxWeight);

            // Assert: Shipment 1 contains the heaviest packages first
            var firstShipment = shipments.First();
            Assert.Contains(firstShipment, p => p.Id == "C");
            Assert.Contains(firstShipment, p => p.Id == "B");
        }
    }
}
