using CourierServiceConsApp.Domain;
using CourierServiceConsApp.Infrastructure;
using CourierServiceConsApp.Services.Implementations;
using Xunit;

namespace CourierService.Tests
{
    public class OfferServiceTests
    {
        private readonly OfferRepository _repo;
        private readonly OfferService _service;

        public OfferServiceTests()
        {
            _repo = new OfferRepository();
            _service = new OfferService(_repo);
        }

        [Fact]
        public void AddOffer_ShouldAddOfferSuccessfully()
        {
            // Arrange
            var offer = new Offer
            {
                Code = "NEW01",
                DiscountPercent = 15,
                MinWeight = 10,
                MaxWeight = 50,
                MinDistance = 20,
                MaxDistance = 200
            };

            // Act
            _service.AddOffer(offer);

            // Assert
            var result = _service.GetOffer("NEW01");
            Assert.NotNull(result);
            Assert.Equal(15, result.DiscountPercent);
        }

        [Fact]
        public void RemoveOffer_ShouldReturnTrue_WhenOfferExists()
        {
            // Arrange
            var existingOffer = _service.GetOffer("OFR001");
            Assert.NotNull(existingOffer);

            // Act
            var removed = _service.RemoveOffer("OFR001");

            // Assert
            Assert.True(removed);
            Assert.Null(_service.GetOffer("OFR001"));
        }

        [Fact]
        public void RemoveOffer_ShouldReturnFalse_WhenOfferDoesNotExist()
        {
            // Act
            var removed = _service.RemoveOffer("DOES_NOT_EXIST");

            // Assert
            Assert.False(removed);
        }

        [Fact]
        public void GetAllOffers_ShouldReturnListOfOffers()
        {
            // Act
            var offers = _service.GetAllOffers();

            // Assert
            Assert.NotEmpty(offers);
        }

        [Fact]
        public void GetOffer_ShouldReturnNull_WhenInvalidCodeProvided()
        {
            // Act
            var result = _service.GetOffer("XYZ123");

            // Assert
            Assert.Null(result);
        }
    }
}
