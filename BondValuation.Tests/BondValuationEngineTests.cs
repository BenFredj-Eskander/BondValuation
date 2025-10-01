using BondValuation.Core;
using BondValuation.Services;
using Xunit;

namespace BondValuation.Tests
{
    public class BondValuationEngineTests
    {
        [Fact]
        public void ValueBonds_WithMixedBondTypes_ReturnsCorrectResults()
        {
            // Arrange
            var services = new IBondValuationService[]
            {
                new CouponBondValuationService(),
                new ZeroCouponBondValuationService()
            };
            var engine = new BondValuationEngine(services);

            var bonds = new[]
            {
                new Bond { BondId = "B1", Type = "Bond", Rate = 0.05, FaceValue = 1000, PaymentsPerYear = 2, YearsToMaturity = 5, DiscountFactor = 0.78 },
                new Bond { BondId = "B2", Type = "Zero-Coupon", Rate = 0.04, FaceValue = 750, YearsToMaturity = 7, DiscountFactor = 0.82 },
                new Bond { BondId = "B3", Type = "Bond", Rate = 0.03, FaceValue = 500, PaymentsPerYear = 4, YearsToMaturity = 3, DiscountFactor = 0.90 },
                new Bond { BondId = "B4", Type = "Zero-Coupon", Rate = 0.07, FaceValue = 1350, YearsToMaturity = 11, DiscountFactor = 0.89 },
            };

            // Act
            var results = engine.ValueBonds(bonds).ToList();

            // Assert
            Assert.Equal(4, results.Count);
            Assert.True(results[0].BondId == "B1" && results[0].Type == "Bond" && results[0].PresentValue == 998.47);
            Assert.True(results[1].BondId == "B2" && results[1].Type == "Zero-Coupon" && results[1].PresentValue == 809.30);
            Assert.True(results[2].BondId == "B3" && results[2].Type == "Bond" && results[2].PresentValue == 492.21);
            Assert.True(results[3].BondId == "B4" && results[3].Type == "Zero-Coupon" && results[3].PresentValue == 2528.98);

        }
    }
}
