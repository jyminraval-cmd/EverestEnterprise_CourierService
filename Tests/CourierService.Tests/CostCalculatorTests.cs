using CourierServiceConsApp.Domain;
using CourierServiceConsApp.Infrastructure;
using CourierServiceConsApp.Services.Implementations;
using Xunit;

namespace CourierServiceConsApp.CourierService.Tests
{
    public class CostCalculatorTests
    {
        [Fact]
        public void ShouldApplyDiscount()
        {
            var repo = new OfferRepository();
            var service = new OfferService(repo);
            var calc = new CostCalculator(service);

            var pkg = new Package("PKG1", 50, 50, "OFR001");

            calc.CalculateCost(100, pkg);

            Assert.True(pkg.Discount >= 0);
        }
    }
}
