using BondValuation.Core;
using Xunit;

namespace BondValuation.Tests
{
    public class BondValuationCoreTests
    {
        [Fact]
        public void Bond_IsValid_WithValidData_ReturnsTrue()
        {
            // Arrange
            var bond = new Bond
            {
                BondId = "TEST001",
                Type = "Bond",
                FaceValue = 1000,
                YearsToMaturity = 5,
                DiscountFactor = 0.78
            };

            // Act
            var isValid = bond.IsValid();

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void Bond_WithInvalidData_IsSkipped()
        {
            // Arrange
            var bond = new Bond
            {
                BondId = "TEST001",
                Type = "Bond",
                FaceValue = 1000,
                YearsToMaturity = 5,
                DiscountFactor = 1.01
            };

            // Act
            var isSkipped = bond.IsSkipped();

            // Assert
            Assert.True(isSkipped);
        }

        [Fact]
        public void Bond_IsValid_WithUnclearIndexing_IsSkipped()
        {
            // Arrange
            var bond = new Bond
            {
                BondId = "TEST001",
                Type = "Inflation-Linked",
                FaceValue = 1000,
                YearsToMaturity = 5,
                DiscountFactor = 0.78,
                DeskNotes = "Indexing details unclear"
            };

            // Act
            var isSkipped = bond.IsSkipped();

            // Assert
            Assert.True(isSkipped);
        }
    }
}
