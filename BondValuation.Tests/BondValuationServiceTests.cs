using BondValuation.Core;
using BondValuation.Services;
using Xunit;

namespace BondValuation.Tests
{
    public class BondValuationServiceTests
    {
        private readonly CouponBondValuationService _couponService;
        private readonly ZeroCouponBondValuationService _zeroCouponService;

        public BondValuationServiceTests()
        {
            _couponService = new CouponBondValuationService();
            _zeroCouponService = new ZeroCouponBondValuationService();
        }

        [Fact]
        public void CalculateValue_CouponBond_ReturnsCorrectPresentValue()
        {
            // Arrange
            var bond = new Bond
            {
                BondId = "TEST001",
                Type = "Bond",
                Rate = 0.022,
                FaceValue = 100,
                PaymentsPerYear = 2,
                YearsToMaturity = 12.8,
                DiscountFactor = 0.56926
            };

            // Act
            var result = _couponService.CalculateValue(bond);

            // Assert
            Assert.Equal(75.33, result.PresentValue); 
        }

        [Fact]
        public void CalculateValue_ZeroCouponBond_ReturnsCorrectPresentValue()
        {
            // Arrange
            var bond = new Bond
            {
                BondId = "TEST002",
                Type = "Zero-Coupon",
                Rate = 0.033,
                FaceValue = 500,
                YearsToMaturity = 9.6,
                DiscountFactor = 0.65537
            };

            // Act
            var result = _zeroCouponService.CalculateValue(bond);

            // Assert
            Assert.Equal(447.53, result.PresentValue); 
        }
    }
}
